// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Diagnostics;

namespace FataAquana
{
	public partial class ApparatenController : NSViewController
	{
		private ApparatenDS ds = null;

		#region Computed Properties
		// Strongly typed view accessor
		public new ApparatenView View
		{
			get
			{
				return (ApparatenView)base.View;
			}
		}
		#endregion

		#region Constructors
		// Called when created from unmanaged code
		public ApparatenController(IntPtr handle) : base (handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public ApparatenController(NSCoder coder) : base (coder)
		{
			Initialize();
		}

		// Call to load from the XIB/NIB file
		public ApparatenController() : base("subviewApparaten", NSBundle.MainBundle)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}
		#endregion

		#region Override Methods
		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			// Create the Occupation Table Data Source and populate it
			ds = new ApparatenDS(AppDelegate.Conn);

			if (ApparatenTable != null)
			{
				// Populate the Product Table
				ApparatenTable.DataSource = ds;
				ApparatenTable.Delegate = new ApparatenDelegate(ds);
			}
		}
		#endregion

		#region Custom Methods
		partial void ApparaatAddClicked(NSButton sender)
		{
			var newApparaat = new ApparaatModel();
			var sheet = new ApparaatEditorSheetController(newApparaat, true);
			// Wire-up
			sheet.ApparaatModified += (apparaat) =>
			{
				// Save person to database
				apparaat.Create(AppDelegate.Conn);

				if (ApparatenTable != null)
				{
					ds.AddApparaat(apparaat);
					ReloadTable();
				}
			};

			// Display sheet
			sheet.ShowSheet(NSApplication.SharedApplication.KeyWindow);
		}

		partial void ApparaatRemoveClicked(NSButton sender)
		{
			// vragen of je zeker ben
			var selectedRowIndex = ApparatenTable.SelectedRow;
			var selectedApparaat = ds.Apparaten[(int)selectedRowIndex] as ApparaatModel;
			ds.Apparaten.Remove(selectedApparaat);
			selectedApparaat.Delete(AppDelegate.Conn);
			ReloadTable();
		}

		public void ReloadTable()
		{
			ApparatenTable.ReloadData();
		}

		[Export("RowDoubleClicked:")]
		public void RowDoubleClicked(NSObject sender)
		{
			var tableView = sender as NSTableView;

			var selectedApparaat = ds.Apparaten[(int)ApparatenTable.SelectedRow] as ApparaatModel;

			Debug.WriteLine("Clicked: " + selectedApparaat.ID + "|" + selectedApparaat.ApparaatNaam);

			var sheet = new ApparaatEditorSheetController(selectedApparaat, true);
			// Wire-up
			sheet.ApparaatModified += (apparaat) =>
			{
				// Save person to database
				apparaat.Update(AppDelegate.Conn);

				if (ApparatenTable != null)
				{
					ReloadTable();
				}
			};

			// Display sheet
			sheet.ShowSheet(NSApplication.SharedApplication.KeyWindow);
		}
		#endregion
	}
}