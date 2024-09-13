


using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace HelloWorldTest
{
    public class UnitTest1
    {


        //Harjoitus - Piirtelyä
        [Fact]
        [Trait("TestGroup", "ArtPrinting")]
        public void ArtPrinting()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Updated expected output to match actual output format
            var expectedOutput = "   *\r\n\r\n   *\r\n  ***\r\n *****\r\n*******";
            var expectedOutput2 = "   *   \r\n       \r\n   *   \r\n  ***  \r\n ***** \r\n*******";


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
                var result = sw.ToString().TrimEnd(); // Trim only the end of the string


                bool matchesExpectedOutput = result == expectedOutput || result == expectedOutput2;

                // Separate assertions with descriptive messages
                Assert.True(matchesExpectedOutput, "output: "
                    + result + "\n what we waite: \n" + expectedOutput);



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


    

