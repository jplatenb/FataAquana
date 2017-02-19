// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Diagnostics;

namespace FataAquana
{
	public partial class PersonenController : NSViewController
	{
		private PersonenDS ds = null;

		public PersoonModel SelectedPersoon = null;

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
		}
		#endregion

		#region Override Methods
		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			// Create the Personen Table Data Source and populate it
			ds = new PersonenDS(AppDelegate.Conn);

			if (PersonenTable != null)
			{
				// Populate the Product Table
				PersonenTable.DataSource = ds;
				PersonenTable.Delegate = new PersonenDelegate(this, ds);
			}
		}
		#endregion
	
		#region Custom Methods
		partial void PersoonAddClicked(Foundation.NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.PersoonAddClicked");

			SelectedPersoon = null;

			PerformSegue("PersoonSegue", this);

			Debug.WriteLine("Einde: PersonenController.PersoonAddClicked");
		}

		partial void PersoonRemoveClicked(Foundation.NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.PersoonRemoveClicked");

			var selectedRowIndex = PersonenTable.SelectedRow;
			var selectedPerson = ds.Personen[(int)selectedRowIndex] as PersoonModel;
			selectedPerson.Delete(AppDelegate.Conn);
			ReloadTable();
		
			Debug.WriteLine("Einde: PersonenController.PersoonRemoveClicked");
		}

		[Export("RowDoubleClicked:")]
		public void RowDoubleClicked(NSObject sender)
		{
			Debug.WriteLine("Start: PersonenController.RowDoubleClicked"); 
			               
			SelectedPersoon = ds.Personen[(int)PersonenTable.SelectedRow] as PersoonModel;

			PerformSegue("PersoonSegue", this);

			Debug.WriteLine("Einde: PersonenController.RowDoubleClicked");
		}

		public void ReloadTable()
		{
			PersonenTable.ReloadData();
		}
		#endregion
	}
}
