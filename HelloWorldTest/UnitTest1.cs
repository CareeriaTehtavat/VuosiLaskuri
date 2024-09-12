


using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HelloWorldTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            Laskin las = new Laskin();
            int num = las.Summa(2, 2);
            Assert.Equal(4, num);

        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(5, 0, 5)]
        [InlineData(0, 5, 5)]
        [Trait("TestGroup", "Adder")]
        public void Adder_Sum_GivesSumOfParameters(int first, int second, int expected)
        {
            // Arrange
            //LaskinClass adder = new LaskinClass();

            // Act
            //var result = adder.Summ(first, second);

            // Assert
            //Assert.Equal(expected, result);

            Laskin las = new Laskin();
            int result = las.Summa(first, second);
            Assert.Equal(expected, result);
        }



        [Fact]
        public void TestStudentPrintsSomethingToConsole()
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
                Task task = Task.Run(() => HelloWorld.Program.Main(new string[0]), cancellationTokenSource.Token);
                task.Wait(cancellationTokenSource.Token);  // Wait for the task to complete or timeout

                // Get the output that was written to the console
                var result = sw.ToString().Trim();

                // Assert
                Assert.False(string.IsNullOrEmpty(result), "The program did not print anything to the console.");
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

