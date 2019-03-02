using IBM.WatsonDeveloperCloud.CompareComply.v1;
using IBM.WatsonDeveloperCloud.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CCWS.CompareComplyV1.CompareComply.IT
{
    public class CompareComplyServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private CompareComplyService service;
        private static string credentials = string.Empty;
        private readonly string versionDate = "2018-11-12";

        private string contract_a = @"CompareComplyV1/CompareComply.IT/CompareComplyTestData/contract_A.pdf";
        private string contract_b = @"CompareComplyV1/CompareComply.IT/CompareComplyTestData/contract_B.pdf";
        private string tableFilePath = @"CompareComplyV1/CompareComply.IT/CompareComplyTestData/TestTable.pdf";
        private string objectStorageCredentialsInputFilepath;
        private string objectStorageCredentialsOutputFilepath;
        public CompareComplyServiceIntegrationTests()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";

                objectStorageCredentialsInputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-input.json";
                objectStorageCredentialsOutputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-output.json";

                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                //VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                //var vcapServices = JObject.Parse(credentials);

                //Credential credential = vcapCredentials.GetCredentialByname("compare-comply-sdk")[0].Credentials;
                endpoint = "https://gateway.watsonplatform.net/compare-comply/api";// credential.Url;
                apikey = "Iiilv-QMhc2BlSbpjSO1buqkrHLjYKMrO_Yy2y0h9LBL";// credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                ServiceUrl = endpoint,
                IamApiKey = apikey
            };

            service = new CompareComplyService(tokenOptions, versionDate);
        }
        [Fact]
        #region Element Classification
        public void ElementClassification_Success()
        {
            using (FileStream fs = File.OpenRead(contract_a))
            {
                var elementClassificationResult = service.ClassifyElements(fs);

                Assert.NotNull(elementClassificationResult);
                Assert.Contains("Microsoft Word - contract_A.doc", elementClassificationResult.Document.Title);
            }
        }
        #endregion

        #region Comparision
        [Fact]
        public void Comparison_Success()
        {
            using (FileStream fs0 = File.OpenRead(contract_a))
            {
                using (FileStream fs1 = File.OpenRead(contract_b))
                {
                    var comparisonResults = service.CompareDocuments(fs0, fs1);

                    Assert.NotNull(comparisonResults);
                    Assert.True(comparisonResults.Documents[0].Title == "Microsoft Word - contract_A.doc");
                    Assert.True(comparisonResults.Documents[1].Title == "Microsoft Word - contract_B.doc");
                }
            }
        }
        #endregion
    }
}
