using CoreProject;
using CoreProject.Clients;
using CoreProject.VoiceCallModel;
using NUnit.Framework;



namespace NUnitTestProject
{
    [TestFixture]
    class CallTests
    {
        [Test]
        public void ShouldCall()
        {
            /*
            var results = CallClient.Do(new Call.CallCommand
            {
                To = new[] {

                    new Call.Endpoint
                    {
                        Type = "phone",
                        Number = ConfigSettings.Instance.Settings["test_number"]
                    }
                },
                From = new Call.Endpoint
                {
                    Type = "phone",
                    Number = ConfigSettings.Instance.Settings["nexmo_number"]
                },
                Answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });
            
            Assert.AreEqual("started", results.status);
            */
        }
    }
}
