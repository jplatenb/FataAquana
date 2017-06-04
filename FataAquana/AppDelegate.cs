using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("AppDelegate")]
	public partial class AppDelegate : NSApplicationDelegate
	{
		public static SqliteConnection Conn = null;

		public static SplitViewController MainView = null;

		public AppDelegate()
		{
			//
		}

		public override void DidFinishLaunching(NSNotification notification)
		{
			// Insert code here to initialize your application
		}

		public override void WillTerminate(NSNotification notification)
		{
			// Insert code here to tear down your application
		}

		public static SqliteConnection GetDatabaseConnection()
		{
			var dlg = NSOpenPanel.OpenPanel;

			dlg.CanChooseFiles = true;
			dlg.CanChooseDirectories = false;

			if (dlg.RunModal() == 1)
			{
				// Nab the first file
				var url = dlg.Urls[0];

				bool exists = File.Exists(url.Path);
				if (!exists)
				{
					SqliteConnection.CreateFile(url.Path);

					Conn = new SqliteConnection("Data Source=" + url.Path);

					var commands = new[] { "CREATE TABLE Opleiding (ID TEXT, Naam TEXT, Omschrijving TEXT)" };
					Conn.Open();

					foreach (var cmd in commands)
					{
						using (var c = Conn.CreateCommand())
						{
							c.CommandText = cmd;
							c.CommandType = CommandType.Text;
							c.ExecuteNonQuery();
						}
					}
					Conn.Close();

				}
				else {
					// Database bestaat al
					Conn = new SqliteConnection("Data Source=" + url.Path);

					try
					{
						Conn.Open();

                        //VoegColumnToe(Conn, "Apparaat", "Omschrijving", "Text");

						//VoegColumnToe(Conn, "Aankoop", "GekochtOpText", "Text");

						//VoegColumnToe(Conn, "Clublidmaatschap", "IngeschrevenOpText", "Text");
						//VoegColumnToe(Conn, "Clublidmaatschap", "UitgeschrevenOpText", "Text");

						//VoegColumnToe(Conn, "GevolgdeOpleiding", "GeslaagdOpText", "Text");
						//VoegColumnToe(Conn, "GevolgdeOpleiding", "VerlopenOpText", "Text");

						//VoegColumnToe(Conn, "InOnderhoud", "OntvangenOpText", "Text");
						//VoegColumnToe(Conn, "InOnderhoud", "RetourOpText", "Text");

						//VoegColumnToe(Conn, "Persoon", "GeboortedatumText", "Text");

						//VoegColumnToe(Conn, "WerkPeriode", "GestartOpText", "Text");
						//VoegColumnToe(Conn, "WerkPeriode", "GestoptOpText", "Text");

						//VerwijderColumn(Conn, "Aankoop", "GekochtOpText");

						//VerwijderColumn(Conn, "Clublidmaatschap", "IngeschrevenOpText");
						//VerwijderColumn(Conn, "Clublidmaatschap", "UitgeschrevenOpText");

						//VerwijderColumn(Conn, "GevolgdeOpleiding", "GeslaagdOpText");
						//VerwijderColumn(Conn, "GevolgdeOpleiding", "VerlopenOpText");

						//VerwijderColumn(Conn, "InOnderhoud", "OntvangenOpText");
						//VerwijderColumn(Conn, "InOnderhoud", "RetourOpText");

						//VerwijderColumn(Conn, "Persoon", "GeboortedatumText");

						//VerwijderColumn(Conn, "WerkPeriode", "GestartOpText");
						//VerwijderColumn(Conn, "WerkPeriode", "GestoptOpText");

						Conn.Close();
					}
					catch(Exception e)
					{
                        Debug.WriteLine(e.Message);
					}

					MainView.EnableGroepList();
				}
			}
			else {

			}

			return Conn;
		}

        internal static void CopyEmailadressen()
        {
            Debug.WriteLine("CopyEmailadressen clicked");

            // Only act when rows selected.

            Debug.WriteLine("Test " + MainView.Description);

            //NSString s = "Test string email adres jplatenb@dxc.com";

   //         NSPasteboard pasteboard = NSPasteboard.GeneralPasteboard;

   //         pasteboard.ClearContents();

			////Debug.WriteLine(MainView.SplitViewItems[1].ViewController.NibName);

			////pasteboard.WriteObjects("Test string email adres jplatenb@dxc.com");
			//// Using a Pasteboard Item
			//NSPasteboardItem item = new NSPasteboardItem();
			//string[] writableTypes = { "joost@platenburg.eu" };

			//// Save to pasteboard
			//pasteboard.WriteObjects(new NSPasteboardItem[] { item });
		}


		private static void VoegColumnToe(SqliteConnection conn, string tablename, string columnname, string columntype)
		{
			try
			{
				var cmd = string.Format("ALTER TABLE {0} ADD {1} {2}", tablename, columnname, columntype);

				using (var c = Conn.CreateCommand())
				{
					c.CommandText = cmd;
					c.CommandType = CommandType.Text;
					c.ExecuteNonQuery();
				}

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
		}

		private static void VerwijderColumn(SqliteConnection conn, string tablename, string columnname)
		{
			try
			{
				var cmd = string.Format("ALTER TABLE {0} DROP COLUMN {1}", tablename, columnname);

				using (var c = Conn.CreateCommand())
				{
					c.CommandText = cmd;
					c.CommandType = CommandType.Text;
					c.ExecuteNonQuery();
				}

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
		}

		public static DateTime NSDateToDateTime(NSDate date)
		{
			// NSDate has a wider range than DateTime, so clip
			// the converted date to DateTime.Min|MaxValue.
			double secs = date.SecondsSinceReferenceDate;
			if (secs < -63113904000)
				return DateTime.MinValue;
			if (secs > 252423993599)
				return DateTime.MaxValue;
			return (DateTime)date;
		}

		public static NSDate DateTimeToNSDate(DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind(date, DateTimeKind.Utc /* or DateTimeKind.Local, this depends on each app */);

			return (NSDate)date;
		}
	}
}
