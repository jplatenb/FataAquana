// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Collections.Generic;
using System.Diagnostics;

namespace FataAquana
{
	public partial class OnderhoudController : NSViewController
	{
		private PersoonController _parentController;
		private InOnderhoudModel _onderhoud;

		public List<InOnderhoudModel> InOnderhoud { get; set; } = new List<InOnderhoudModel>();

		public OnderhoudController (IntPtr handle) : base (handle)
		{
		}

		#region Override Methods
		public override void AwakeFromNib()
		{
			Debug.WriteLine("Start: OnderhoudController.AwakeFromNib");

			_parentController = this.PresentingViewController as PersoonController;
			if (_parentController != null)
			{
				Onderhoud = _parentController.SelectedOnderhoud;

				if (Onderhoud == null) Onderhoud = new InOnderhoudModel();

				if (OnderhoudCombobox != null)
				{
					OnderhoudCombobox.UsesDataSource = true;
					OnderhoudCombobox.Completes = true;
					OnderhoudCombobox.DataSource = new ApparatenComboDS();

					if (!Onderhoud.ApparaatNaam.Equals(string.Empty))
					{
						var index = OnderhoudCombobox.DataSource.IndexOfItem(OnderhoudCombobox, Onderhoud.ApparaatNaam);
						OnderhoudCombobox.SelectItem(index);
					}
				}

				if (OntvangenOpButton != null)
				{
					OntvangenOpButton.State = NSCellStateValue.Off;
					OntvangenOpDate.Enabled = false;
				}

				if (RetourOpButton != null)
				{
					RetourOpButton.State = NSCellStateValue.Off;
					RetourOpDate.Enabled = false;
				}
			}
			Debug.WriteLine("Start: OnderhoudController.AwakeFromNib");
		}

		[Export("Onderhoud")]
		public InOnderhoudModel Onderhoud
		{
			get { return _onderhoud; }
			set
			{
				WillChangeValue("Onderhoud");
				_onderhoud = value;
				DidChangeValue("Onderhoud");
			}
		}
		#endregion

		partial void CloseButton(NSObject sender)
		{
			Debug.WriteLine("Start: OnderhoudController.CloseButton");

			DismissController(this);

			Debug.WriteLine("Einde: OnderhoudController.CloseButton");
		}

		partial void SaveButton(NSObject sender)
		{
			Debug.WriteLine("Start: OnderhoudController.SaveButton");

			if (OnderhoudCombobox.DataSource != null)
			{
				ApparatenComboDS comboDS = OnderhoudCombobox.DataSource as ApparatenComboDS;

				var selectedApparaat = comboDS.Apparaten[(int)OnderhoudCombobox.SelectedIndex];

				Onderhoud.PersoonID = _parentController.Persoon.ID;
				Onderhoud.ApparaatID = selectedApparaat.ID;
				if (OntvangenOpButton.State.Equals(NSCellStateValue.On))
				{
					Onderhoud.OntvangenOp = OntvangenOpDate.DateValue;
				}
				if (RetourOpButton.State.Equals(NSCellStateValue.On))
				{
					Onderhoud.RetourOp = RetourOpDate.DateValue;
				}

				Onderhoud.Create(AppDelegate.Conn);

				if (_parentController != null)
				{
					_parentController.LoadTables();
				}
			}

			DismissController(this);

			Debug.WriteLine("Einde: OnderhoudController.SaveButton");
		}

		partial void OntvangenOpEnable(NSObject sender)
		{
			if (OntvangenOpButton.State.Equals(NSCellStateValue.On))
			{
				OntvangenOpDate.Enabled = true;
			}
			else
			{
				OntvangenOpDate.Enabled = false;
			}
		}

		partial void RetourOpEnable(NSObject sender)
		{
			if (RetourOpButton.State.Equals(NSCellStateValue.On))
			{
				RetourOpDate.Enabled = true;
			}
			else
			{
				RetourOpDate.Enabled = false;
			}
		}
	}
}
