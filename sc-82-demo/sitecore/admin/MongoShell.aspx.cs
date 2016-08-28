using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using Sitecore.Diagnostics;
using Sitecore.ExperienceContentManagement.Administration;

namespace sc_82_demo.sitecore.admin
{
    public partial class MongoShell : NonSecurePage
    {
        #region Private fields
        enum MongoOperations
        {
            /// <summary>
            /// Test Connection
            /// </summary>
            TC,
            /// <summary>
            /// Find All Users
            /// </summary>
            FAC,
            /// <summary>
            /// Get Collection Names
            /// </summary>
            GCN,
            /// <summary>
            /// Get Stats
            /// </summary>
            GS,
            /// <summary>
            /// Get Collection
            /// </summary>
            GC
        }
        private readonly StringBuilder Log = new StringBuilder();

        #endregion

        #region Protected methods or events
        protected override void OnInit(EventArgs arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            base.CheckSecurity(true);
            base.OnInit(arguments);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings)
                {
                    if (IsMongoConnectionString(setting))
                    {
                        ddlMongoDBs.Items.Add(new ListItem(setting.Name, setting.Name));

                        //TestConnectionString(setting.Name, setting.ConnectionString);
                    }
                }
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            try
            {


                //foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings)
                //{
                //    if (IsMongoConnectionString(setting))
                //    {
                //        TestConnectionString(setting.Name, setting.ConnectionString);
                //    }
                //}

                if (ddlMongoDBs.SelectedIndex == -1 || ddlOperations.SelectedIndex == -1)
                {
                    Log.AppendLine();
                    // Show Messsage
                    Log.AppendLine("No MongoDB Selected or No Operation selected. Please select");
                }
                else
                {
                    MongoOperations mongoOperations = (MongoOperations)
                        Enum.Parse(typeof(MongoOperations), ddlOperations.SelectedItem.Value, true);

                    var connectionString = ConfigurationManager.ConnectionStrings[ddlMongoDBs.SelectedItem.Value].ConnectionString;
                    switch (mongoOperations)
                    {
                        case MongoOperations.TC:
                            TestConnectionString(ddlMongoDBs.SelectedItem.Text, connectionString);
                            break;
                        case MongoOperations.FAC:
                        case MongoOperations.GCN:
                        case MongoOperations.GS:
                            DoMongoOperation(ddlMongoDBs.SelectedItem.Text, connectionString, mongoOperations);
                            break;
                        case MongoOperations.GC:
                            if (string.IsNullOrWhiteSpace(txtInput.Text))
                                Log.AppendLine("For Get collection operation. Collection Name is required. Please provide it via Input box");
                            else
                                DoMongoOperation(ddlMongoDBs.SelectedItem.Text, connectionString, mongoOperations);
                            break;
                        default:
                            Log.AppendLine("Invalid operation");
                            break;
                    }

                }

            }
            catch (Exception exception)
            {
                Log.AppendLine($"{exception.Message}{exception.InnerException}{exception.StackTrace}");

            }
            DumpLog(Log);
        }

        #endregion

        #region Private methods
        private void DoMongoOperation(string name, string connectionString, MongoOperations mongoOperation)
        {
            Log.AppendLine($"====== Test Date: {DateTime.Now.ToString(CultureInfo.InvariantCulture)} ======");
            Log.AppendLine($"Connection Name: {name}.");
            Log.AppendLine($"Connection String: {connectionString}.");

            try
            {
                MongoClient client = new MongoClient(connectionString);

                // http://stackoverflow.com/questions/7201847/how-to-get-the-mongo-database-specified-in-connection-string-in-c-sharp
                // TODO : Authentication
                var database = client.GetServer().GetDatabase(new MongoUrl(connectionString).DatabaseName);

                Log.AppendLine($"====== Operation Name: {ddlOperations.SelectedItem} ======");
                switch (mongoOperation)
                {
                    case MongoOperations.FAC:
                        var users = database.FindAllUsers();

                        foreach (MongoUser mongouser in users)
                        {
                            Log.AppendLine($"Mongo Username: {mongouser.Username}.");
                        }
                        break;
                    case MongoOperations.GCN:
                        var collectionNames = database.GetCollectionNames();
                        foreach (string collectionName in collectionNames)
                        {
                            Log.AppendLine($"Mongo Collection Name: {collectionName}.");
                        }
                        break;
                    case MongoOperations.GS:
                        var stats = database.GetStats();

                        Log.AppendLine($"CollectionCount: {stats.CollectionCount}.");
                        Log.AppendLine($"DataSize: {Sitecore.StringUtil.GetSizeString(stats.DataSize)}.");
                        //Log.AppendLine($"FileSize: {Sitecore.StringUtil.GetSizeString(stats.FileSize)}.");
                        Log.AppendLine($"IndexSize: {Sitecore.StringUtil.GetSizeString(stats.IndexSize)}.");
                        Log.AppendLine($"IndexCount: {stats.IndexCount}.");
                        Log.AppendLine($"OK?: {stats.Ok}.");
                        Log.AppendLine($"StorageSize: {Sitecore.StringUtil.GetSizeString(stats.StorageSize)}.");
                        break;
                    case MongoOperations.GC:
                        var collection = database.GetCollection(txtInput.Text);
                        foreach (BsonDocument document in collection.FindAll())
                        {
                            Log.AppendLine($"Mongo Document : {document.ToString()}.");
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception exception)
            {
                Log.AppendLine($"{exception.Message}{exception.InnerException}{exception.StackTrace}{mongoOperation.ToString()}");
            }
        }
        
        /// <summary>
        /// If string contains tracking it is Mongo
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        private static bool IsMongoConnectionString(ConnectionStringSettings setting)
        {
            return setting.ConnectionString.StartsWith("mongodb://");
        }

        private void TestConnectionString(string name, string connectionString)
        {
            Log.AppendLine($"====== Test Date: {DateTime.Now.ToString(CultureInfo.InvariantCulture)} ======");
            Log.AppendLine($"Connection Name: {name}.");
            Log.AppendLine($"Connection String: {connectionString}.");

            try
            {
                MongoClient client = new MongoClient(connectionString);

                if (client != null && client.GetServer() != null)
                {
                    if (client.GetServer().State == MongoServerState.Connected)
                    {
                        var versionString = client.GetServer().BuildInfo.VersionString;
                        Log.AppendLine($"MongoDB version: {versionString}.");
                        Log.AppendLine("Wohoo -- Connection is successful!");
                    }
                    else
                    {
                        Log.AppendLine("Ooops -- Unable to make connection.");
                    }
                }
               
            }
            catch (Exception exception)
            {

                Log.AppendLine($"{exception.Message}{exception.InnerException}{exception.StackTrace}");
            }
        }

        private void DumpLog(StringBuilder log)
        {

            txtResult.Text = log.ToString();
            //Console.WriteLine(log);

            //var fileName = $"Support.Log.{DateTime.Now.ToString("yyyyMMdd.hhmmss")}.txt";

            //using (StreamWriter writer = new StreamWriter(fileName))
            //{
            //    writer.Write(log);
            //}
        }
        #endregion
    }
}