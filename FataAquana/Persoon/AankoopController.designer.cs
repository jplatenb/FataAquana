// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FataAquana
{
	[Register ("AankoopController")]
	partial class AankoopController
	{
		[Outlet]
		AppKit.NSComboBox ApparatenCombobox { get; set; }

		[Outlet]
		AppKit.NSDatePicker GekochtOpDate { get; set; }

		[Outlet]
		AppKit.NSTextField GekochtOpText { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("SaveButton:")]
		partial void SaveButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ApparatenCombobox != null) {
				ApparatenCombobox.Dispose ();
				ApparatenCombobox = null;
			}

			if (GekochtOpDate != null) {
				GekochtOpDate.Dispose ();
				GekochtOpDate = null;
			}

			if (GekochtOpText != null) {
				GekochtOpText.Dispose ();
				GekochtOpText = null;
			}
		}
	}
}
