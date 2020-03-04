using UnityEngine;
using System.Collections;

public class GameConstants : MonoBehaviour {

	public static bool isGameOver = false;
	public static bool isGamePause = false;
	public static bool isGameStarted=false;

	public static int levelNo = 1;
	public static int carNo = 1;
	public static int misstakeCounter = 0;

	public static bool levelComplete = false;
	public static bool levelFailed = false;
	public static bool timeup = false;
	public static bool crashed = false;

	public static bool goingToLevelSelection=false;


	public static void ResetAllStatic()
	{
		levelComplete = false;
		levelFailed = false;
		timeup = false;
		crashed = false;
		isGameOver = false;
		isGamePause = false;
		levelNo = 0;
		carNo = 1;
		misstakeCounter = 0;
	}

	public static void ResetAllStatic2()
	{
		levelComplete = false;
		levelFailed = false;
		timeup = false;
		crashed = false;
		isGameOver = false;
		isGamePause = false;
		isGameStarted = false;
		misstakeCounter = 0;

	}
}
