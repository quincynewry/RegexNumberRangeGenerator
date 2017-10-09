using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace RegexNumberRangeGenerator.Tests
{
    [TestClass]
    public class RegexNumberRangeGeneratorTest
    {
        [TestMethod]
        public void Generate_1To100With99_Pass()
        {
            // Assemble
            int min = 1;
            int max = 100;
            string testString = "some text 99 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsTrue(regex.IsMatch(testString));
        }

        [TestMethod]
        public void Generate_1To100With101_Fail()
        {
            // Assemble
            int min = 1;
            int max = 100;
            string testString = "some text 101 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsFalse(regex.IsMatch(testString));
        }

        [TestMethod]
        public void Generate_777To888With799_Pass()
        {
            // Assemble
            int min = 777;
            int max = 888;
            string testString = "some text 799 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsTrue(regex.IsMatch(testString));
        }

        [TestMethod]
        public void Generate_777To888With899_Fail()
        {
            // Assemble
            int min = 777;
            int max = 888;
            string testString = "some text 899 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsFalse(regex.IsMatch(testString));
        }

        [TestMethod]
        public void Generate_65000To66000With65123_Pass()
        {
            // Assemble
            int min = 65000;
            int max = 66000;
            string testString = "some text 65123 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsTrue(regex.IsMatch(testString));
        }

        [TestMethod]
        public void Generate_65000To66000With66001_Fail()
        {
            // Assemble
            int min = 65000;
            int max = 66000;
            string testString = "some text 66001 to test";

            // Act
            string regexPattern = RegexNumberRangeGenerator.Generate(min, max);

            // Assert
            Regex regex = new Regex(regexPattern);

            Assert.IsFalse(regex.IsMatch(testString));
        }
    }
}
