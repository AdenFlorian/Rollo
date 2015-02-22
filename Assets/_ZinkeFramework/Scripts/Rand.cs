using UnityEngine;
using System.Collections;

public static class Rand {

	static string[] firstNames = new string[] {
        "Cleta",
        "Denna",
        "Quinn",
        "Corrina",
        "Cassey",
        "Keren",
        "Tessie",
        "Helen",
        "Nevada",
        "Milagro",
        "Katherine",
        "Nickole",
        "Albina",
        "Lizabeth",
        "Ladawn",
        "Denyse",
        "Linette",
        "Reed",
        "Serita",
        "Leilani",
        "Shirely",
        "Kristine",
        "Antonette",
        "Ebonie",
        "Danille",
        "Luanne",
        "Marget",
        "Meredith",
        "Araceli",
        "Leatrice",
        "Maple",
        "Elvis",
        "Heather",
        "Annita",
        "Hortencia",
        "Bethanie",
        "Lindsy",
        "Emilie",
        "Reynalda",
        "Lorretta",
        "Lennie",
        "Nikki",
        "Shantel",
        "Granville",
        "Linnea",
        "Tenesha",
        "Eustolia",
        "Garrett",
        "Lyn",
        "Dominick"
	};

	public static Vector3 RangedVector3(float min, float max) {
		Vector3 newVec3 = new Vector3();
		newVec3.x = Random.Range(min, max);
		newVec3.y = Random.Range(min, max);
		newVec3.z = Random.Range(min, max);
		return newVec3;
	}

	public static string StrName() {
		return firstNames[Random.Range(0, firstNames.Length)];
	}

	public static void RandomizeSeed() {
		Random.seed = System.DateTime.Now.Second;
	}
}

