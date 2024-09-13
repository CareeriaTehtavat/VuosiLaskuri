


using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace HelloWorldTest
{
    public class UnitTest1
    {
       

        //Harjoitus - Hello Nimi!

        [Fact]
        [Trait("TestGroup", "MyNamePrinting")]

        public void MyNamePrinting()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            string expectedOutput = "Hello !";

            // Set a timeout of 30 seconds for the test execution
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

            try
            {
                // Act
                Task task = Task.Run(() =>
                {
                    // Run the program
                    HelloWorld.Program.Main(new string[0]);
                }, cancellationTokenSource.Token);

                task.Wait(cancellationTokenSource.Token);  // Wait for the task to complete or timeout

                // Get the output that was written to the console
                var result = sw.ToString().Trim();
                var words = result.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                // Assert: Check if the result matches the expected output
                Assert.False(string.IsNullOrEmpty(result), "The program did not print anything to the console.");
                //Assert.Equal(expectedOutput, result);
                Assert.Contains("Hello", words);
                Assert.Equal(2, words.Count);

            }
            catch (OperationCanceledException)
            {
                Assert.True(false, "The operation was canceled due to timeout.");
            }
            catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
            {
                Assert.True(false, "The operation was canceled due to timeout.");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }



      
    }
}


    

