using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers.Abstractions;
using Db1.Helpers;
using Db1.Services.Abstract;

namespace Db1.CommandHandlers
{
    public class InsertCommandHandler : Db1CommandHandlerBase<InsertCommand, InsertCommandExecutionResult>
    {
        private readonly ITableDefinitionService _tableDefinitionService;
        private readonly IInsertService _insertService;

        public InsertCommandHandler(
            ITableDefinitionService tableDefinitionService,
            IInsertService insertService)
        {
            _tableDefinitionService = tableDefinitionService ?? throw new ArgumentNullException(nameof(tableDefinitionService));
            _insertService = insertService ?? throw new ArgumentNullException(nameof(insertService));
        }

        protected override async Task<InsertCommandExecutionResult> ExecuteAsync(InsertCommand command)
        {
            var tableDefinition = await _tableDefinitionService.GetTableDefinitionAsync(command.TableName);
            var dataToInsert = CollectDataToInsert(command, tableDefinition);
            await _insertService.ExecuteAsync(dataToInsert, tableDefinition);

            return new InsertCommandExecutionResult($"{command.Rows.Length} entries have been successfully inserted into '{command.TableName}' table.", command.Rows.Length);
        }

        private List<string> CollectDataToInsert(InsertCommand command, TableDefinition tableDefinition)
        {
            return command.Rows
                .Select(row => CreateEntryFromRow(command, tableDefinition, row))
                .ToList();
        }

        private static string CreateEntryFromRow(InsertCommand command, TableDefinition tableDefinition, string[] cells)
        {
            var entryBuilder = new StringBuilder();
            var cellIndex = 0;

            var tableColumns = tableDefinition.Columns.ToArray();
            foreach (var tableColumn in tableColumns)
            {
                var cellValue = command.Columns.Contains(tableColumn.Name, StringComparer.OrdinalIgnoreCase)
                    ? GetTableColumnValue(tableColumn, cells[cellIndex++])
                    : GetTableColumnEmptyValue(tableColumn);
                
                entryBuilder.AppendFormat(cellValue);
            }

            return entryBuilder.ToString();
        }

        private static string GetTableColumnValue(Column tableColumn, string cellValue)
        {
            if (cellValue.Length > tableColumn.Size)
            {
                throw new InvalidOperationException($"The size of '{tableColumn.Name}' column is less than the size of provided value which is {cellValue.Length}");
            }

            if (cellValue.Length == tableColumn.Size)
            {
                return $"{cellValue}|";
            }

            var lack = tableColumn.Size - cellValue.Length;
            return $"{cellValue}{StringHelper.GetStringOfSigns(lack, ' ')}|";
        }

        private static string GetTableColumnEmptyValue(Column tableColumn)
        {
            return $"{StringHelper.GetStringOfSigns(tableColumn.Size, ' ')}|";
        }
    }
}