using System;
using System.Data;
using System.Collections.Generic;

using NUnit.Framework;

namespace HbMailer.Jobs.Impl {
  [TestFixture]
  class RecipientResolverTests {
    MailJob job;
    DataTable data;
    RecipientResolver resolver;

    [SetUp]
    public void RecipientResolverSetup() {

      job = new MailJob() {
        Query = @"
          SELECT
            'Marvin Countryman' AS recipientName
          , 'me@maar.vin'       AS recipientEmail
          , 'He is a cool dude' AS recipientMetadata
          UNION ALL
          SELECT
            'Marvin Countryman'      AS recipientName
          , 'spam@maar.vin'          AS recipientEmail
          , 'He is the coolest dude' AS recipientMetadata
          UNION ALL
          SELECT
            'Marvin Countryman'  AS recipientName
          , 'spam@maar.vin'      AS recipientEmail
          , 'He is a narcissist' AS recipientMetadata
        ",
        NameColumn = "recipientName",
        EmailColumn = "recipientEmail",
      };

      data = new DataTable();
      data.Columns.Add("recipientName");
      data.Columns.Add("recipientEmail");
      data.Columns.Add("recipientMetadata");

      data.Rows.Add("Marvin Countryman", "me@maar.vin", "He is a cool dude");
      data.Rows.Add("Marvin Countryman", "spam@maar.vin", "He is the coolest dude");
      data.Rows.Add("Marvin Countryman", "spam@maar.vin", "He is a narcissist");

      resolver = new RecipientResolver();
    }

    // TODO: Automate SQL query testing

    [Test]
    public void TestFormatData() {

      List<MailJobRecipient> recipients = resolver.FormatData(
        data,
        job
      );

      Assert.AreEqual(3, recipients.Count);
      Assert.AreEqual("me@maar.vin", recipients[0].Email);
      Assert.AreEqual(
        "He is a cool dude",
        recipients[0].MergeFields["recipientMetadata"]
      );
    }
  }
}
