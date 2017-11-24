using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticWise.Compiler.Components.Pagination;
using Shouldly;
using System.Text;

namespace StaticWise.Compiler.Components.Tests.Pagination
{
    [TestClass]
    public class StandardPaginationTests
    {
        #region Constants

        const string CONTAINER_OPEN_HTML = "<ul class=\"pagination\">";
        const string CONTAINER_CLOSE_HTML = "</ul>";
        const string ITEM_OPEN_HTML = "<li>";
        const string ITEM_CLOSE_HTML = "</li>";
        const string FIRST_FILENAME = "index";
        const string ARCHIVE_FILENAME = "page";
        const string ARCHIVE_DIRECTORY_NAME = "archive";

        #endregion

        #region Properties

        IPagination _pagination;

        #endregion

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _pagination = new StandardPagination();
        }

        #endregion

        #region Generate

        [TestMethod]
        public void Generate_ShouldBeEmpty_IfTotalIs1OrLess()
        {
            _pagination.Generate(
                0,
                0,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(string.Empty);

            _pagination.Generate(
                1,
                1,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(string.Empty);
        }

        [TestMethod]
        public void Generate_ShouldBeEmpty_IfTotalIsLessThanOrEqualToPaginationCount()
        {
            _pagination.Generate(
                1,
                8,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(string.Empty);

            _pagination.Generate(
                1,
                10,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(string.Empty);
        }

        [TestMethod]
        public void Generate_ShouldBeRootDirectory_IfArchiveDirectoryNameIsEmpty()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}11.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}11.html\">11</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}12{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}13.html\">13</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}14.html\">14</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}15.html\">15</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_FILENAME}13.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                12,
                15,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                string.Empty)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBeFirstFileName_IfPreviousIsFirstPage()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}2{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);
            
            _pagination.Generate(
                2,
                2,
                1,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBeArchiveName_IfPreviousIsNotFirstPage()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}3{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                3,
                3,
                1,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBeRewindOf1_IfCurrentIs2()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}2{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}3.html\">3</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}3.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                2,
                3,
                2,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBeRewindOf2_IfCurrentIs3()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}3{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                3,
                3,
                2,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIs1AndTotalIs10()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}1{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}3.html\">3</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">4</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                1,
                12,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIs2AndTotalIs11()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}2{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}3.html\">3</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">4</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}3.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                2,
                11,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIs3AndTotalIs11()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}3{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">4</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                3,
                11,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIsGreaterThan3AndTotalIs20()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">4</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}8{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}11.html\">11</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}12.html\">12</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}13.html\">13</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                8,
                20,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIs12AndTotalIs15()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}11.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}9.html\">9</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}11.html\">11</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}12{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}13.html\">13</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}14.html\">14</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}15.html\">15</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}13.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                12,
                15,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldBe10Pages_IfCurrentIs9AndTotalIs20()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}6.html\">6</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}7.html\">7</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}8.html\">8</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}9{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">10</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}11.html\">11</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}12.html\">12</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}13.html\">13</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}14.html\">14</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}10.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                9,
                20,
                10,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldNotContainNext_IfCurrentIsEqualToTotal()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}3{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                3,
                3,
                1,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        [TestMethod]
        public void Generate_ShouldContainNext_IfTotalIsGreaterThanCurrent()
        {
            StringBuilder b = new StringBuilder();
            b.Append(CONTAINER_OPEN_HTML);
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">Previous</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{FIRST_FILENAME}.html\">1</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}2.html\">2</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}3{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">4</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}5.html\">5</a>{ITEM_CLOSE_HTML}");
            b.Append($"{ITEM_OPEN_HTML}<a href=\"/{ARCHIVE_DIRECTORY_NAME}/{ARCHIVE_FILENAME}4.html\">Next</a>{ITEM_CLOSE_HTML}");
            b.Append(CONTAINER_CLOSE_HTML);

            _pagination.Generate(
                3,
                5,
                2,
                CONTAINER_OPEN_HTML,
                CONTAINER_CLOSE_HTML,
                ITEM_OPEN_HTML,
                ITEM_CLOSE_HTML,
                FIRST_FILENAME,
                ARCHIVE_FILENAME,
                ARCHIVE_DIRECTORY_NAME)
                .ShouldBe(b.ToString());
        }

        #endregion
    }
}
