using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherUtil
{
	public enum SwitcherInputType
	{
		Numbers,
		QWERTY,
		ASDFGH,
		ZXCVBN
	}

	private static string NUMBER_STR = "1234567890";
	private static string QWERTY_STR = "QWERTYUIOP";
	private static string ASDFGH_STR = "ASDFGHJKL;";
	private static string ZXCVBN_STR = "ZXCVBNM,./";

	private static KeyCode[] NUMBER_KEYS = new KeyCode[10]
	{
		KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
	};
	private static KeyCode[] QWERTY_KEYS = new KeyCode[10]
	{
		KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P
	};
	private static KeyCode[] ASDFGH_KEYS = new KeyCode[10]
	{
		KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon
	};
	private static KeyCode[] ZXCVBN_KEYS = new KeyCode[10]
	{
		KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash
	};

	public static char GetKeyStr(SwitcherInputType switcherType, int index)
	{
		if(index < 0 || index > 9) return '\0';

		switch(switcherType)
		{
			case SwitcherInputType.Numbers:
				return NUMBER_STR[index];
			case SwitcherInputType.QWERTY:
				return QWERTY_STR[index];
			case SwitcherInputType.ASDFGH:
				return ASDFGH_STR[index];
			case SwitcherInputType.ZXCVBN:
				return ZXCVBN_STR[index];
			default:
				return '\0';
		}
	}

	public static int GetInputIndex(SwitcherInputType switcherType)
	{
		KeyCode[] keys;
		switch(switcherType)
		{
			case SwitcherInputType.Numbers:
				keys = NUMBER_KEYS;
				break;
			case SwitcherInputType.QWERTY:
				keys = QWERTY_KEYS;
				break;
			case SwitcherInputType.ASDFGH:
				keys = ASDFGH_KEYS;
				break;
			case SwitcherInputType.ZXCVBN:
				keys = ZXCVBN_KEYS;
				break;
			default:
				return -1;
		}

		for(int i = 0; i < keys.Length; i++)
		{
			if(Input.GetKeyDown(keys[i]))
				return i;
		}

		return -1;
	}
}

public abstract class Switcher<SwitchType> : MonoBehaviour {

	public List<SwitchType> m_switchObjectList;
	public List<string> m_names;
	public SwitcherUtil.SwitcherInputType m_switcherDebugInputType;
	public int m_startIndex = 0;
	public bool m_autoSwitchOnStart = false;

	protected int m_currentIndex;

	private static string m_debugName;

	void Start()
	{
		if(m_autoSwitchOnStart)
		{
			SwitchAllObjects(
				m_switchObjectList[Mathf.Min(m_startIndex, m_switchObjectList.Count-1)]
			);
		}
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10 + 100 * (int) m_switcherDebugInputType, 10, 110, 500));
		GUILayout.BeginVertical();

		for(int i = 0; i < m_switchObjectList.Count; i++)
		{
			bool current = m_currentIndex == i;
			string entryName;
				if(i < m_names.Count)
				{
					entryName = m_names[i];
				}
				else if(m_switchObjectList[i] != null)
				{
					entryName = m_switchObjectList[i].ToString();
				}
				else
				{
					entryName = "???";
				}

			GUILayout.Label(string.Format(
				"{0}{1} - {2}{3}",
				current ? "[" : "",
				SwitcherUtil.GetKeyStr(m_switcherDebugInputType, i),
				entryName,
				current ? "]" : ""
			));
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update () {

		int input = SwitcherUtil.GetInputIndex(m_switcherDebugInputType);

		if(input >= 0)
		{
			SwitchAllObjects(input);
		}
	}

	public void SwitchAllObjects(int index)
	{
		if(index < m_switchObjectList.Count)
		{
			m_currentIndex = index;
			SwitchAllObjects(m_switchObjectList[index]);
		}
	}

	protected abstract void SwitchAllObjects(SwitchType newObjPrefab);
}

public class PipeSwitcher : Switcher<Pipe> {

	public PipeSpawner m_pipeSpawner;

	protected override void SwitchAllObjects(Pipe newPipePrefab)
	{
		m_pipeSpawner.SwitchAllPipes(newPipePrefab);
	}
}
