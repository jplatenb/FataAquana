// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Diagnostics;

namespace FataAquana
{
	public partial class PersonenController : NSViewController
	{
		public PersoonModel SelectedPersoon = null;
		public PersonenDS dsPersonen = null;
        public NSTableView personentable = null;

		#region Computed Properties

		#endregion

		#region Constructors
		// Called when created from unmanaged code
		public PersonenController(IntPtr handle) : base (handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public PersonenController(NSCoder coder) : base (coder)
		{
			Initialize();
		}

		// Call to load from the XIB/NIB file
		public PersonenController() : base("subviewPersonen", NSBundle.MainBundle)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
            AppDelegate.personencontroller = this;
		}
		#endregion

		#region Override Methods
		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			if (PersonenTable != null)
			{
				personentable = PersonenTable;
				// Create the Personen Table Data Source and populate it
				dsPersonen = new PersonenDS(AppDelegate.Conn);

				// Populate the Product Table
				PersonenTable.DataSource = dsPersonen;
				PersonenTable.Delegate = new PersonenDelegate(this, dsPersonen);
			}

		}
		#endregion
	
		#region Custom Methods
		partial void PersoonAddClicked(Foundation.NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.PersoonAddClicked");

			SelectedPersoon = null;

			PerformSegue("PersoonNewSegue", this);

			Debug.WriteLine("Einde: PersonenController.PersoonAddClicked");
		}

		partial void PersoonRemoveClicked(Foundation.NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.PersoonRemoveClicked");

			if ((int)PersonenTable.SelectedRow >= 0)
			{
				var selectedRowIndex = PersonenTable.SelectedRow;
				SelectedPersoon = dsPersonen.Personen[(int)PersonenTable.SelectedRow] as PersoonModel;

				// Configure alert
				var alert = new NSAlert()
				{
					AlertStyle = NSAlertStyle.Informational,
					InformativeText = $"Weet je zeker dat je de persoon {SelectedPersoon.Achternaam} wilt verwijderen?\n\nDit kan niet meer ongedaan gemaakt worden.",
					MessageText = $"Delete {SelectedPersoon.Achternaam}?",
				};
				alert.AddButton("Cancel");
				alert.AddButton("Delete");
				alert.BeginSheetForResponse(this.View.Window, (result) =>
				{
					// Should we delete the requested row?
					if (result == 1001)
					{
						// Remove the given row from the dataset
						SelectedPersoon.Delete(AppDelegate.Conn);
						dsPersonen.Personen.Remove(SelectedPersoon);
						ReloadTable();
					}
				});
			}
			Debug.WriteLine("Einde: PersonenController.PersoonRemoveClicked");
		}

		[Export("RowDoubleClicked:")]
		public void RowDoubleClicked(NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.RowDoubleClicked"); 
			               
			SelectedPersoon = dsPersonen.Personen[(int)PersonenTable.SelectedRow] as PersoonModel;

			PerformSegue("PersoonSegue", this);

			Debug.WriteLine("Einde: PersonenController.RowDoubleClicked");
		}

		public void ReloadTable()
		{
			Debug.WriteLine("Start: PersonenController.ReloadTable");

			if (PersonenTable != null)
			{
				personentable = PersonenTable;
				// Create the Personen Table Data Source and populate it
				dsPersonen = new PersonenDS(AppDelegate.Conn);

				// Populate the Product Table
				PersonenTable.DataSource = dsPersonen;
				PersonenTable.Delegate = new PersonenDelegate(this, dsPersonen);
			}

            PersonenTable.ReloadData();
			
            Debug.WriteLine("Einde: PersonenController.ReloadTable");
		}
		#endregion
	}
}
