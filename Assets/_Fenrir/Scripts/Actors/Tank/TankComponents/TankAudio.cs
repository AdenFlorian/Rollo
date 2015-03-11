
public class TankAudio : TankComponent {
	protected void Update() {
		GetComponent<UnityEngine.AudioSource>().pitch = tank.mover.speedNormalized;
	}
}
