using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherUtil
{
	public enum SwitcherType
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

	public static char GetKeyStr(SwitcherType switcherType, int index)
	{
		if(index < 0 || index > 9) return '\0';

		switch(switcherType)
		{
			case SwitcherType.Numbers:
				return NUMBER_STR[index];
			case SwitcherType.QWERTY:
				return QWERTY_STR[index];
			case SwitcherType.ASDFGH:
				return ASDFGH_STR[index];
			case SwitcherType.ZXCVBN:
				return ZXCVBN_STR[index];
			default:
				return '\0';
		}
	}

	public static int GetInput(SwitcherType switcherType)
	{
		KeyCode[] keys;
		switch(switcherType)
		{
			case SwitcherType.Numbers:
				keys = NUMBER_KEYS;
				break;
			case SwitcherType.QWERTY:
				keys = QWERTY_KEYS;
				break;
			case SwitcherType.ASDFGH:
				keys = ASDFGH_KEYS;
				break;
			case SwitcherType.ZXCVBN:
				keys = ZXCVBN_KEYS;
				break;
			default:
				return -1;
		}

		for(int i = 0; i <= 9; i++)
		{
			if(Input.GetKeyDown(keys[i]))
				return i;
		}

		return -1;
	}
}

public abstract class Switcher<Type> : MonoBehaviour {

	public List<GameObject> m_switchObjectList;
	public List<string> m_names;
	public SwitcherUtil.SwitcherType m_switcherDebugInputType;
	public int m_startIndex = 0;

	protected int m_currentIndex;

	private static string m_debugName;

	void Start()
	{
		SwitchAllObjects(
			m_switchObjectList[Mathf.Min(m_currentIndex, m_switchObjectList.Count-1)]
		);
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10 + 100 * (int) m_switcherDebugInputType, 10, 110, 500));
		GUILayout.BeginVertical();

		for(int i = 0; i < m_switchObjectList.Count; i++)
		{
			bool current = m_currentIndex == i;
			string entryName = (i < m_names.Count) ? m_names[i] : m_switchObjectList[i].name;
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

		int input = SwitcherUtil.GetInput(m_switcherDebugInputType);

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
			SwitchAllObjects(m_switchObjectList[index - 1]);
		}
	}

	protected abstract void SwitchAllObjects(GameObject newObjPrefab);
}

public class PipeSwitcher : Switcher<Pipe> {

	public PipeSpawner m_pipeSpawner;

	protected override void SwitchAllObjects(GameObject newPipePrefab)
	{
		// Find all Pipe objects and replace them
		Transform pipeRoot = m_pipeSpawner.m_pipesRoot.transform;
		int numPipes = pipeRoot.childCount;
		Pipe[] originalPipes = pipeRoot.GetComponentsInChildren<Pipe>();

		if(numPipes > 0)
		{
			// Instantiate first new pipe
			Transform firstPipeTransform = pipeRoot.GetChild(0);
			GameObject firstNewPipe = GameObject.Instantiate(newPipePrefab);
			firstNewPipe.transform.position = firstPipeTransform.position;
			firstNewPipe.transform.rotation = firstPipeTransform.rotation;
			firstNewPipe.transform.localScale = firstPipeTransform.localScale;

			// Destroy original pipes
			for(int i = originalPipes.Length - 1; i >= 0; i--)
			{
				GameObject.Destroy(originalPipes[i].gameObject);
			}

			// Add first pipe
			firstNewPipe.transform.SetParent(pipeRoot);

			// Instantiate the rest of the f***ing owl
			Pipe prevPipe = firstNewPipe.GetComponent<Pipe>();
			Debug.Assert(prevPipe != null, "PipeSwitcher: cannot switch to object without Pipe component");
			for(int i = 1; i < numPipes; i++)
			{
				// Place each new pipe at the end of the previous pipe
				GameObject newPipe = GameObject.Instantiate(newPipePrefab);
				newPipe.transform.position = prevPipe.transform.position + 
					(prevPipe.EndTransform.localPosition - prevPipe.StartTransform.localPosition);
				newPipe.transform.rotation = prevPipe.transform.rotation;
				newPipe.transform.localScale = prevPipe.transform.localScale;
				newPipe.transform.SetParent(pipeRoot);
				prevPipe = newPipe.GetComponent<Pipe>();
			}
		}

		if(m_pipeSpawner != null)
		{
			m_pipeSpawner.m_pipePrefab = newPipePrefab;
		}
	}
}
