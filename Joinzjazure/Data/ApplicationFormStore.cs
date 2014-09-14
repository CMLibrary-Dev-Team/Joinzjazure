﻿using System.Collections.Generic;
using Joinzjazure.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Joinzjazure.Data
{
    public class ApplicationFormStore
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public ApplicationFormStore()
        {
            _connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            _tableName = CloudConfigurationManager.GetSetting("AzureTableName");
        }

        public IEnumerable<FormEntity> GetAll()
        {
            CloudTable table = GetTable();
            TableQuery<FormEntity> query =
                new TableQuery<FormEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.GreaterThan, "0"));
            return table.ExecuteQuery(query);
        }

        public void Save(ApplicationForm form)
        {
            var table = GetTable();

            var entity = new FormEntity(form);
            TableOperation operation = TableOperation.InsertOrReplace(entity);
            table.Execute(operation);
        }

        private CloudTable GetTable()
        {
#if DEBUG
            var account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            var account = CloudStorageAccount.Parse(_connectionString);
#endif
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference(_tableName);
            return table;
        }
    }
}