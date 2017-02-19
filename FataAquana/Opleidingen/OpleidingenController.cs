// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Diagnostics;

namespace FataAquana
{
	public partial class OpleidingenController : NSViewController
	{
		private OpleidingenDS ds = null;

		#region Computed Properties
		// Strongly typed view accessor
		//public new OpleidingenView View
		//{
		//	get
		//	{
		//		return (OpleidingenView)base.View;
		//	}
		//}
		#endregion

		#region Constructors
		// Called when created from unmanaged code
		public OpleidingenController(IntPtr handle) : base (handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public OpleidingenController(NSCoder coder) : base (coder)
		{
			Initialize();
		}

		// Call to load from the XIB/NIB file
		public OpleidingenController() : base("subviewOpleidingen", NSBundle.MainBundle)
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
			//var DataSource = new TableORMDatasource(AppDelegate.Conn);
			ds = new OpleidingenDS(AppDelegate.Conn);

			if (OpleidingenTable != null)
			{
				// Populate the Product Table
				OpleidingenTable.DataSource = ds;
				OpleidingenTable.Delegate = new OpleidingenDelegate(ds);
			}
		}
		#endregion

		#region Custom Methods
		partial void OpleidingAddClicked(NSButton sender)
		{
			//var newOpleiding = new OpleidingModel();

			//var sheet = new OpleidingEditorSheetController(newOpleiding, true);
			//// Wire-up
			//sheet.OpleidingModified += (opleiding) =>
			//{
			//	// Save club to database
			//	opleiding.Create(AppDelegate.Conn);

			//	if (OpleidingenTable != null)
			//	{
			//		ds.AddOpleiding(opleiding);

			//		ReloadTable();
			//	}
			//};

			//// Display sheet
			//sheet.ShowSheet(NSApplication.SharedApplication.KeyWindow);
		}

		partial void OpleidingRemoveClicked(NSButton sender)
		{
			// vragen of je zeker ben
			var selectedRowIndex = OpleidingenTable.SelectedRow;
			var selectedOpleiding = ds.Opleidingen[(int)selectedRowIndex] as OpleidingModel;
			ds.Opleidingen.Remove(selectedOpleiding);
			selectedOpleiding.Delete(AppDelegate.Conn);
			ReloadTable();
		}

		public void ReloadTable()
		{
			OpleidingenTable.ReloadData();
		}

		[Export("RowDoubleClicked:")]
		public void RowDoubleClicked(NSObject sender)
		{
			//var selectedOpleiding = ds.Opleidingen[(int)OpleidingenTable.SelectedRow] as OpleidingModel;

			//Debug.WriteLine("Clicked: " + selectedOpleiding.ID + "|" + selectedOpleiding.OpleidingNaam);

			//var sheet = new OpleidingEditorSheetController(selectedOpleiding, true);
			//// Wire-up
			//sheet.OpleidingModified += (opleiding) =>
			//{
			//	// Save club to database
			//	opleiding.Update(AppDelegate.Conn);

			//	if (OpleidingenTable != null)
			//	{
			//		ReloadTable();
			//	}
			//};

			//// Display sheet
			//sheet.ShowSheet(NSApplication.SharedApplication.KeyWindow);
		}
		#endregion
	}
}
