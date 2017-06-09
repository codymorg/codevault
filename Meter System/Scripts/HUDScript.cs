using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour {

    //All the public variables.
	public Image sanity;
	public float Sanity = 1;
	public float loseSanity;
	public float RegainSanity;

	//public Slider SanitySlider;
	//public Text SanityText;
	//public string SanityString;
	public Image energy;
	public float Energy = 1;
	public float EnergyLoss;
	public float EnergyGain;

	//public Slider FatigueSl;
	//public Text FatigueText;
	//public string FatigueS;
	public Image hunger;
	public float Hunger = 1;
	public float HungerLoss;
	public float HungerGain;

	//public Slider HungerSl;
//	public Text HungerText;
//	public string HungerS;
	public Image health;
	public float Health = 1;
	public float HealthLoss;
	public float HealthGain;

	private Vector3 PlayerLastPosition;
	void Update () {
        SanityFunction();
        HungerAndFatigue();
        UIupdate ();

		//This is a piece of code for testing and should be removed.
		if (Input.GetKeyDown (KeyCode.Z) && Hunger > 0)
            Hunger -= HungerGain;
		if (Input.GetKeyDown (KeyCode.X) && Health > 0)
			Health -= HealthGain;
		if (Input.GetKeyDown (KeyCode.C) && Health < 1)
			Health += HealthLoss;
	}

	/*Simply put this just updates the UIs fade in and out based on
	 * sanity, hunger, and fatgiue levels.
	 */
	public void UIupdate(){
		sanity.color = new Color (sanity.color.r, sanity.color.g, sanity.color.b, Sanity);
		//SanitySlider.value = Sanity;
		//SanityString = Sanity.ToString();
		//SanityText.text = SanityString;


		hunger.color = new Color (hunger.color.r, hunger.color.g, hunger.color.b, Hunger);
		//HungerSl.value = Hunger;
		//HungerS = Hunger.ToString();
		//HungerText.text = FatigueS;

		energy.color = new Color (energy.color.r, energy.color.g, energy.color.b, Energy);
		//FatigueSl.value = Fatigue;
		//FatigueS = Fatigue.ToString();
		//FatigueText.text = FatigueS;

		health.color = new Color (health.color.r, health.color.g, health.color.b, Health);
	}

    public void UpdateMeters(float[] itemEffects )
    {
        Sanity -= itemEffects[0];
        Energy -= itemEffects[1];
        Hunger -= itemEffects[2];
        Health -= itemEffects[3];
    }

	/*Tracks a player's sanity levels. Based off of if anything with a collider is above the player
	 *such as a roof or anything.But this would also apply to things such as cliff overhangs // caves
	 *even a bird flying overhead for that split second they are directly above the player and if they
	 *have colliders. This function should be updated as we really define where a player needs to be
	 *to gain sanity.
	 */
	public void SanityFunction(){
		RaycastHit hit;
		Ray SanityRay = new Ray (transform.position, Vector3.up);
		Debug.DrawRay (transform.position, Vector3.up,Color.red);
        //prevents bug that raised Sanity above 1 and wouldn't allow it to come down
        if (Sanity > 1)
            Sanity = 1;
        //if you're inside sanity goes up
		if (Physics.Raycast (SanityRay, out hit)) {
			if(Sanity <= 1 && Sanity >= 0)
			    Sanity -= RegainSanity;
		}
        else {
			if (Energy > 0){
			Energy -= EnergyGain;
			}

			if(Sanity <= 1){
			Sanity += loseSanity;
			}
		}
	}

	/*Tracks if player has moved and updates hunger and fatigue accordingly
	 *This also tracks the Y axis so if a player is falling it will also make
	 *them hungry and lose energy. But it will also make them spend hunger and
	 *energy doing such things like climbing ladders. If it becomes a problem
	 *special rules can be applied.
	 */
	public void HungerAndFatigue(){
		if (PlayerLastPosition != transform.position && Energy < 1) {
			Energy += EnergyLoss;
		}

		if (PlayerLastPosition != transform.position && Hunger < 1) {
			Hunger += HungerLoss;
		}
		PlayerLastPosition = transform.position;
	}
}
