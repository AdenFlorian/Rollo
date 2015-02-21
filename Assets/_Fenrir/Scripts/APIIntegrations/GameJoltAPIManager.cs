using UnityEngine;
using System.Collections;

public enum GJTrophyEx {
	WhiteRabbit = 16384,
	StartFire = 16442
}

public class GameJoltAPIManager : MonoBehaviour {

	public static GameJoltAPIManager Inst;
	public int gameID;
	public string privateKey;
	bool userIsVerified = false;
	GJTrophy[] userTrophies;

	void Awake() {
		GJAPI.Users.VerifyCallback += OnVerifyUser;
		GJAPI.Trophies.GetAllCallback += OnGetAllTrophies;
		GJAPI.Trophies.AddCallback += OnAddTrophy;

		Inst = this;

		DontDestroyOnLoad(gameObject);

		GJAPI.Init(gameID, privateKey);

#if UNITY_WEBPLAYER
		GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
#endif
	}

	// Callbacks
	void OnVerifyUser(bool success) {
		if (success) {
			Debug.Log("Yepee!");
			GJAPIHelper.Users.ShowGreetingNotification();
			userIsVerified = true;
			GJAPI.Trophies.GetAll();
		} else {
			Debug.Log("Um... Something went wrong.");
		}
	}
	void OnGetFromWeb(string name, string token) {
		if (name != "" && token != "") {
			Debug.Log("GJAPI: Verifying " + name + "@" + token);
			GJAPI.Users.Verify(name, token);
		}
	}
	void OnGetAllTrophies(GJTrophy[] trophies) {
		userTrophies = trophies;
	}
	void OnAddTrophy(bool success) {
		
	}

	public void AwardTrophy(GJTrophyEx trophy) {
		if (userIsVerified) {
			if (!DoesUserHaveTrophy(trophy)) {
				GJAPI.Trophies.Add((uint)trophy);
				GJAPIHelper.Trophies.ShowTrophyUnlockNotification((uint)trophy);
			} else {
				Debug.Log("User already has trophy: " + trophy.ToString());
			}
		}
	}

	bool DoesUserHaveTrophy(GJTrophyEx trophyEx) {
		foreach (GJTrophy trophy in userTrophies) {
			if (trophy.Id == (uint)trophyEx) {
				if (trophy.Achieved) {
					return true;
				} else {
					return false;
				}
			}
		}
		return false;
	}
}
