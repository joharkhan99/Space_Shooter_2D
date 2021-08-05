using UnityEngine;

public static class GameData {
	private static int _diamonds = 0;
	private static int _golds = 0;

	//Static Constructor to load data from playerPrefs
	static GameData ( ) {
		_diamonds = PlayerPrefs.GetInt ("DIAMOND", 0 );
		_golds = PlayerPrefs.GetInt ( "GOLD", 0 );
	}

	public static int Diamonds
	{
		get{ return _diamonds; }
		set{ PlayerPrefs.SetInt ( "DIAMOND", (_diamonds = value) ); }
	}

	public static int Golds {
		get{ return _golds; }
		set{ PlayerPrefs.SetInt ( "GOLD", (_golds = value) ); }
	}

	/*---------------------------------------------------------
		this line:
		set{ PlayerPrefs.SetInt ( "Gems", (_gems = value) ); }

		is equivalent to:
		set{ 
			_gems = value;
			PlayerPrefs.SetInt ( "Gems", _gems ); 
		}
	------------------------------------------------------------*/
}
