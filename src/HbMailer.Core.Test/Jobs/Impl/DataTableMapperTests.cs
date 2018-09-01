using System;
using System.Data;
using System.Collections.Generic;

using NUnit.Framework;

namespace HbMailer.Jobs.Impl {
  [TestFixture]
  class DataTableMapperTests {
    DataTable data;
    DataTableMapper mapper;

    [SetUp]
    public void DataTableMapperTestSetup() {
      data = new DataTable();
      data.Columns.Add("columnName1");
      data.Columns.Add("columnName2");
      data.Columns.Add("columnName3");
      data.Columns.Add("columnName4");

      mapper = new DataTableMapper();
    }

    [Test]
    public void TestNoMappable() {

      DataTableMapResult result = mapper.MapColumns(
        data,
        new List<string>()
      );

      Assert.IsEmpty(result.Columns);
      Assert.AreEqual(4, result.UnmappedColumns.Count);

    }

    [Test]
    public void TestAllMappable() {

      DataTableMapResult result = mapper.MapColumns(
        data,
        new List<string>() {
          "columnName1",
          "columnName2",
          "columnName3",
          "columnName4"
        }
      );

      Assert.AreEqual(4, result.Columns.Count);
      Assert.IsEmpty(result.UnmappedColumns);
    }

    [Test]
    public void TestFirstMappable() {

      DataTableMapResult result = mapper.MapColumns(
        data,
        new List<string>() {
          "columnName1",
        }
      );

      Assert.AreEqual(1, result.Columns.Count);
      Assert.AreEqual(3, result.UnmappedColumns.Count);
    }

    [Test]
    public void TestLastMappable() {

      DataTableMapResult result = mapper.MapColumns(
        data,
        new List<string>() {
          "columnName4",
        }
      );

      Assert.AreEqual(1, result.Columns.Count);
      Assert.AreEqual(3, result.UnmappedColumns.Count);
    }

    [Test]
    public void TestMissingColumnNames() {
      Assert.Throws<InvalidOperationException>(() => mapper.MapColumns(
        data,
        new List<string>() {
          "InValId CoLuMn NaMe!~"
        }
      ));

      Assert.DoesNotThrow(() => mapper.MapColumns(
        data,
        new List<string>() {
          "InValId CoLuMn NaMe!~"
        },
        false
      ));
    }
  }
}
