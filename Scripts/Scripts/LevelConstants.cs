using UnityEngine;
using System.Collections;

public class LevelConstants : MonoBehaviour {

	public static string[] levelObjective = {
        "Go To Gas Station For Refuel",
        "Go for Picking Kids From Park And Drop Them on The House",
        "Call city mall driver to drop you at parking lot",
        "Set into Car to reach Your Home",
		"Objective 5",
		"Objective 6",
		"Objective 7",
		"Objective 8",
		"Objective 9",
		"Objective 10"
	};


	public static int[] levelReward = {500,1000,1500,2000,2500,3000,3500,4000,4500,5000};
	public static int[] levelTime = { 90,105, 130, 150,165,180,200,220,240,255};
	public static string[] levelTime_LS={ "01:30","01:45","02:10","02:30","02:45","03:00","03:20","03:40","04:00","04:15"};
	public static int[] levelMistakes = {8,8,7,7,6,6,6,5,5,5,100};
}
