


using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace HelloWorldTest
{
    public class UnitTest1
    {


        //Harjoitus - Tekstimuuttujat
        [Fact]
        [Trait("TestGroup", "NameTesting")]
        public void NameTesting()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

    
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

                var resultLines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                string[] requiredWords = { "Minun", "nimeni", "on", "ja", "asun", "kaupungissa" };

                bool firstLinePass = requiredWords.All(word => resultLines[0].Contains(word));



                int secondLineWordCount = CountWords(resultLines[1]);
                bool secondPass = secondLineWordCount == 1; // Replace length check with word count check

                int threedWords = CountWords(resultLines[2]);
                bool threedPass = threedWords == 2; // Replace length check with word count check

                // Assert
                Assert.True(firstLinePass, "firstLine not Pass" + result);
                Assert.True(secondPass, "secondnot Pass" + result + " / " + secondLineWordCount);
                Assert.True(threedPass, "threed not Pass" + result + " / " + threedWords);

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
        private int CountWords(string line)
        {
            return line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private bool CompareLines(string[] actualLines, string[] expectedLines)
        {
            if (actualLines.Length != expectedLines.Length)
            {
                return false;
            }

            for (int i = 0; i < actualLines.Length; i++)
            {
                if (actualLines[i] != expectedLines[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}


    

