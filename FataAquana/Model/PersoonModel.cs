﻿using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using Foundation;
using AppKit;
using System.Collections.Generic;
using System.Diagnostics;
using Mono;

namespace FataAquana
{
	public class PersonenModel : List<PersoonModel>
	{
		
	}

	[Register("Persoon")]
	public class PersoonModel : NSObject
	{
		#region Private Variables
		private string _ID = string.Empty;
		private string _achternaam = string.Empty;
		private string _voornamen = string.Empty;
		private string _tussenvoegsel = string.Empty;
		private string _initialen = string.Empty;

		private string _adres = string.Empty;
		private string _postcode = string.Empty;
		private string _woonplaats = string.Empty;
		private string _telefoon = string.Empty;
		private string _mobiel = string.Empty;
		private string _email = string.Empty;
		private NSDate _geboortedatum = new NSDate();
        private string _imagepath = string.Empty;

		private NSMutableArray _gevolgdeopleidingen = new NSMutableArray();
		private string _gevolgdeopleidingenstring = string.Empty;
		private NSMutableArray _aankopen = new NSMutableArray();
		private string _aankopenstring = string.Empty;
		private NSMutableArray _inonderhoud = new NSMutableArray();
		private string _inonderhoudstring = string.Empty;
		private NSMutableArray _lidmaatschappen = new NSMutableArray();
		private string _lidmaatschappenstring = string.Empty;

		private SqliteConnection _conn = null;
		#endregion

		#region Computed Properties
		public SqliteConnection Conn {
			get { return _conn; }
			set { _conn = value; }
		}

		[Export("ID")]
		public string ID {
			get { return _ID; }
			set
			{
                if (_ID == value) return;

				WillChangeValue("ID");
                _ID = value;
				DidChangeValue("ID");
			}
		}

		[Export("Achternaam")]
		public string Achternaam { 
			get { return _achternaam; }
			set
			{
                if (_achternaam == value) return;

                WillChangeValue("Achternaam");
				_achternaam = value;
				DidChangeValue("Achternaam");

				// Save changes to database
				if (_conn != null) Update(_conn);
			} 
		}

		[Export("Adres")]
		public string Adres {
			get { return _adres; }
			set
			{
                if (_adres == value) return;

				WillChangeValue("Adres");
				_adres = value;
				DidChangeValue("Adres");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Geboortedatum")]
		public NSDate Geboortedatum {
			get { return _geboortedatum; }
			set
			{
				if (_geboortedatum == value) return;

				WillChangeValue("Geboortedatum");
				_geboortedatum = value;
				DidChangeValue("Geboortedatum");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Imagepath")]
		public string Imagepath {
			get { return _imagepath; }
			set
			{
				if (_imagepath == value) return;

				WillChangeValue("Imagepath");
				_imagepath = value;
				DidChangeValue("Imagepath");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Initialen")]
		public string Initialen {
			get { return _initialen; }
			set
			{
				if (_initialen == value) return;

				WillChangeValue("Initialen");
				_initialen = value;
				DidChangeValue("Initialen");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Postcode")]
		public string Postcode {
			get { return _postcode; }
			set
			{
				if (_postcode == value) return;

				WillChangeValue("Postcode");
				_postcode = value;
				DidChangeValue("Postcode");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Tussenvoegsel")]
		public string Tussenvoegsel {
			get { return _tussenvoegsel; }
			set
			{
				if (_tussenvoegsel == value) return;

				WillChangeValue("Tussenvoegsel");
				_tussenvoegsel = value;
				DidChangeValue("Tussenvoegsel");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Voornamen")]
		public string Voornamen { 
			get { return _voornamen; }
			set
			{
				if (_voornamen == value) return;

				WillChangeValue("Voornamen");
				_voornamen = value;
				DidChangeValue("Voornamen");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Woonplaats")]
		public string Woonplaats {
			get { return _woonplaats; }
			set
			{
				if (_woonplaats == value) return;

				WillChangeValue("Woonplaats");
				_woonplaats = value;
				DidChangeValue("Woonplaats");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Telefoon")]
		public string Telefoon {
			get { return _telefoon; }
			set
			{
				if (_telefoon == value) return;

				WillChangeValue("Telefoon");
				_telefoon = value;
				DidChangeValue("Telefoon");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Mobiel")]
		public string Mobiel {
			get { return _mobiel; }
			set
			{
				if (_mobiel == value) return;

				WillChangeValue("Mobiel");
				_mobiel = value;
				DidChangeValue("Mobiel");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Email")]
		public string Email {
			get { return _email; }
			set
			{
				if (_email == value) return;

				WillChangeValue("Email");
				_email = value;
				DidChangeValue("Email");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GevolgdeOpleidingen")]
		public NSMutableArray GevolgdeOpleidingen
		{
			get { return _gevolgdeopleidingen; }
			set
			{
				if (_gevolgdeopleidingen == value) return;

				WillChangeValue("GevolgdeOpleidingen");
				WillChangeValue("GevolgdeOpleidingenString");
				_gevolgdeopleidingen = value;
				DidChangeValue("GevolgdeOpleidingenString");
				DidChangeValue("GevolgdeOpleidingen");
			}
		}

		[Export("GevolgdeOpleidingenString")]
		public string GevolgdeOpleidingenString
		{
			get {
                if (GevolgdeOpleidingen.Count > 0)
                {
                    _gevolgdeopleidingenstring = string.Empty;

                    foreach (var obj in NSArray.FromArray<NSObject>(GevolgdeOpleidingen))
                    {
                        var gevopl = obj as GevolgdeOpleidingModel;
                        DateTime dt = AppDelegate.NSDateToDateTime(gevopl.GeslaagdOp);
                        _gevolgdeopleidingenstring = string.Format("{0} ({1:yyyy-MM-dd}), {2}", gevopl.OpleidingNaam, dt, _gevolgdeopleidingenstring);
                    }

                    if (_gevolgdeopleidingenstring.Length > 2)
    				{
	    				_gevolgdeopleidingenstring = _gevolgdeopleidingenstring.Substring(0, (_gevolgdeopleidingenstring.Length - 2));
		    		}
				}

                return _gevolgdeopleidingenstring; 
            }
		}

		[Export("Aankopen")]
		public NSMutableArray Aankopen
		{
			get { return _aankopen; }
			set
			{
				if (_aankopen == value) return;

				WillChangeValue("Aankopen");
				WillChangeValue("AankopenString");
				_aankopen = value;
				DidChangeValue("AankopenString");
				DidChangeValue("Aankopen");
			}
		}

		[Export("AankopenString")]
		public string AankopenString
		{
			get
			{
				if (Aankopen.Count > 0)
				{
					_aankopenstring = string.Empty;

					foreach (var obj in NSArray.FromArray<NSObject>(Aankopen))
					{
						var aankoop = obj as AankoopModel;
						DateTime dt = AppDelegate.NSDateToDateTime(aankoop.GekochtOp);
						_aankopenstring = string.Format("{0} ({1:yyyy-MM-dd}), {2}", aankoop.ApparaatNaam, dt, _aankopenstring);
					}

					if (_aankopenstring.Length > 2)
					{
						_aankopenstring = _aankopenstring.Substring(0, (_aankopenstring.Length - 2));
					}
				}

				return _aankopenstring;
			}
		}

		[Export("InOnderhoud")]
		public NSMutableArray InOnderhoud
		{
			get { return _inonderhoud; }
			set
			{
				if (_inonderhoud == value) return;

				WillChangeValue("InOnderhoud");
				WillChangeValue("InOnderhoudString");
				_inonderhoud = value;
				DidChangeValue("InOnderhoudString");
				DidChangeValue("InOnderhoud");
			}
		}

		[Export("InOnderhoudString")]
		public string InOnderhoudString
		{
			get { 
                if (InOnderhoud.Count > 0)
                {
                    _inonderhoudstring = string.Empty;

                    foreach (var obj in NSArray.FromArray<NSObject>(InOnderhoud))
                    {
                        var inonderhoud = obj as InOnderhoudModel;
							DateTime dt = AppDelegate.NSDateToDateTime(inonderhoud.OntvangenOp);
							_inonderhoudstring = string.Format("{0} ({1:yyyy-MM-dd}), {2}", inonderhoud.ApparaatNaam, dt, _inonderhoudstring);
						}

                    if (_inonderhoudstring.Length > 2)
                    {
                        _inonderhoudstring = _inonderhoudstring.Substring(0, (_inonderhoudstring.Length - 2));
                    }
                }

                return _inonderhoudstring;
            }
		}

		[Export("Lidmaatschappen")]
		public NSMutableArray Lidmaatschappen
		{
			get { return _lidmaatschappen; }
			set
			{
				if (_lidmaatschappen == value) return;

				WillChangeValue("Lidmaatschappen");
				WillChangeValue("LidmaatschappenString");
				_lidmaatschappen = value;
				DidChangeValue("LidmaatschappenString");
				DidChangeValue("Lidmaatschappen");
			}
		}

		[Export("LidmaatschappenString")]
		public string LidmaatschappenString
		{
			get { 
                if (Lidmaatschappen.Count > 0)
                {
					_lidmaatschappenstring = string.Empty;

					foreach (var obj in NSArray.FromArray<NSObject>(Lidmaatschappen))
					{
						var lidmaatschap = obj as ClublidmaatschapModel;
						DateTime dt = AppDelegate.NSDateToDateTime(lidmaatschap.IngeschrevenOp);
						_lidmaatschappenstring = string.Format("{0} ({1:yyyy-MM-dd}), {2}", lidmaatschap.ClubNaam, dt, _lidmaatschappenstring);
					}

					if (_lidmaatschappenstring.Length > 2)
					{
						_lidmaatschappenstring = _lidmaatschappenstring.Substring(0, (_lidmaatschappenstring.Length - 2));
					}
				}

                return _lidmaatschappenstring;
}
		}
		#endregion

		#region Constructors
		public PersoonModel()
		{
		}

		public PersoonModel(string achternaam, string voornamen, string tussenvoegsel)
		{
			this.Achternaam = achternaam;
			this.Voornamen = voornamen;
			this.Tussenvoegsel = tussenvoegsel;
		}
		#endregion

		#region SQLite Routines
		public void Create(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Create new record ID?
			if (ID == "")
			{
				ID = Guid.NewGuid().ToString();
			}

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "INSERT INTO [Persoon] (ID, Achternaam, Adres, Email, " +
										"Geboortedatum, Imagepath, Initialen, Mobiel, Postcode, " +
										"Telefoon, Tussenvoegsel, Voornamen, Woonplaats) " +
									"VALUES (@COL1, @COL2, @COL3, @COL4, @COL5, @COL6, @COL7, @COL8, @COL9, @COL10, @COL11, @COL12, @COL13)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", Achternaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Email);
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(Geboortedatum));
				command.Parameters.AddWithValue("@COL6", Imagepath);
				command.Parameters.AddWithValue("@COL7", Initialen);
				command.Parameters.AddWithValue("@COL8", Mobiel);
				command.Parameters.AddWithValue("@COL9", Postcode);
				command.Parameters.AddWithValue("@COL10", Telefoon);
				command.Parameters.AddWithValue("@COL11", Tussenvoegsel);
				command.Parameters.AddWithValue("@COL12", Voornamen);
				command.Parameters.AddWithValue("@COL13", Woonplaats);

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Update(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "UPDATE [Persoon] SET Achternaam = @COL2, Adres = @COL3, Email  = @COL4, " +
					"Geboortedatum = @COL5, Imagepath = @COL6, Initialen = @COL7, Mobiel = @COL8, Postcode = @COL9, " +
					"Telefoon = @COL10, Tussenvoegsel = @COL11, Voornamen = @COL12, Woonplaats = @COL13 WHERE ID =  @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", Achternaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Email);
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(Geboortedatum));
				command.Parameters.AddWithValue("@COL6", Imagepath);
				command.Parameters.AddWithValue("@COL7", Initialen);
				command.Parameters.AddWithValue("@COL8", Mobiel);
				command.Parameters.AddWithValue("@COL9", Postcode);
				command.Parameters.AddWithValue("@COL10", Telefoon);
				command.Parameters.AddWithValue("@COL11", Tussenvoegsel);
				command.Parameters.AddWithValue("@COL12", Voornamen);
				command.Parameters.AddWithValue("@COL13", Woonplaats);

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Load(SqliteConnection conn, string id)
		{
			//Debug.WriteLine("PersoonModel.Load(" + id + ")");

			bool shouldClose = false;

			// clear last connection to preventcirculair call to update
			_conn = null;

			// Is the database already open?
			if (conn.State != ConnectionState.Open)
			{
				shouldClose = true;
				conn.Open();
			}

			// Execute query
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "SELECT ID, Achternaam, Adres, Email, " +
					"Geboortedatum, Imagepath, Initialen, Mobiel, Postcode, " +
					"Telefoon, Tussenvoegsel, Voornamen, Woonplaats FROM Persoon WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					var r = reader.HasRows;

					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader["ID"];
						Achternaam = (string)reader["Achternaam"];
						Adres = (string)reader["Adres"];
						Email = (string)reader["Email"];
						Geboortedatum = AppDelegate.DateTimeToNSDate((DateTime)reader["Geboortedatum"]);
						Imagepath = (string)reader["Imagepath"];
						Initialen = (string)reader["Initialen"];
						Mobiel = (string)reader["Mobiel"];
						Postcode = (string)reader["Postcode"];
						Telefoon = (string)reader["Telefoon"];
						Tussenvoegsel = (string)reader["Tussenvoegsel"];
						Voornamen = (string)reader["Voornamen"];
						Woonplaats = (string)reader["Woonplaats"];

                        // ook de 'gevolgde opleidingen' laden
                        LoadGevolgdeOpleidingen(conn, ID);

                        // ook de 'aankopen' laden
                        LoadAankopen(conn, ID);

						// ook de 'onderhoud' laden
						//						var onderhoudArray = new OnderhoudArrayController();
						//						onderhoudArray.LoadOnderhoud(conn, ID);
						//						InOnderhoud = onderhoudArray.Onderhoud;

						// ook de 'clubs' laden
						//						var lidmaatschappenArray = new LidmaatschappenArrayController();
						//						lidmaatschappenArray.LoadLidmaatschappen(conn, ID);
						//						Lidmaatschappen = lidmaatschappenArray.Lidmaatschappen;
					}
				}
			}

            ////Debug.WriteLine("Joost: " + GevolgdeOpleidingenString);

			if (shouldClose)
			{
				conn.Close();
			}

			// Save last connection
			_conn = conn;
		}

        public void LoadGevolgdeOpleidingen(SqliteConnection conn, string id)
        {
			bool shouldClose = false;

			// clear last connection to preventcirculair call to update
			_conn = null;

			// Is the database already open?
			if (conn.State != ConnectionState.Open)
			{
				shouldClose = true;
				conn.Open();
			}

			using (var commandGO = conn.CreateCommand())
			{
				try
				{
					// Create new command
					commandGO.CommandText = "SELECT DISTINCT ID FROM [GevolgdeOpleiding] WHERE PersoonID = '" + ID + "'";
					using (var readerGO = commandGO.ExecuteReader())
					{
						while (readerGO.Read())
						{
							var gevopl = new GevolgdeOpleidingModel();
							var idGO = (string)readerGO["ID"];

							gevopl.Load(conn, idGO);

							GevolgdeOpleidingen.Add(gevopl);
						}
					}
				}
				catch (Exception Exception)
				{
					Debug.WriteLine(Exception.Message);
				}
			}

            if (shouldClose)
			{
				conn.Close();
			}

			// Save last connection
			_conn = conn;
		}

		public void LoadAankopen(SqliteConnection conn, string id)
		{
			bool shouldClose = false;

			// clear last connection to preventcirculair call to update
			_conn = null;

			// Is the database already open?
			if (conn.State != ConnectionState.Open)
			{
				shouldClose = true;
				conn.Open();
			}

			using (var commandAK = conn.CreateCommand())
			{
				try
				{
					// Create new command
					commandAK.CommandText = "SELECT DISTINCT ID FROM [Aankoop] WHERE PersoonID = '" + ID + "'";
					using (var readerAK = commandAK.ExecuteReader())
					{
						while (readerAK.Read())
						{
							var aankoop = new AankoopModel();
							var idAK = (string)readerAK["ID"];

							aankoop.Load(conn, idAK);

							Aankopen.Add(aankoop);
						}
					}
				}
				catch (Exception Exception)
				{
					Debug.WriteLine(Exception.Message);
				}
			}

			if (shouldClose)
			{
				conn.Close();
			}

			// Save last connection
			_conn = conn;
		}
		
        public void Delete(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "DELETE FROM [Persoon] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			Achternaam = "";
			Adres = "";
			Email = "";
			Geboortedatum = new NSDate();
			Imagepath = "";
			Initialen = "";
			Mobiel = "";
			Postcode = "";
			Telefoon = "";
			Tussenvoegsel = "";
			Voornamen = "";
			Woonplaats = "";

			// Save last connection
			_conn = conn;
		}
		#endregion

	}
}

