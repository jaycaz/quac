using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pipe))]
public class PipeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Pipe myScript = (Pipe)target;
        if(GUILayout.Button("Refresh Materials"))
        {
            myScript.AutoFillMaterials();
        }
    }
}

[System.Serializable]
public class Pipe : MonoBehaviour {

	public Transform StartTransform { get { return m_startTransform; } }
	[SerializeField] private Transform m_startTransform;

	public Transform EndTransform { get { return m_endTransform; } }
	[SerializeField] private Transform m_endTransform;

	public Material m_darkMaterial;
	public Material m_lightMaterial;

	private struct Pair<T1, T2>
	{
		public T1 first;
		public T2 second;

		public Pair(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}
	}
	private List<Pair<Renderer, int>> m_darkMaterialRefs = new List<Pair<Renderer, int>>();
	private List<Pair<Renderer, int>> m_lightMaterialRefs = new List<Pair<Renderer, int>>();

	void Start () {

		if(m_darkMaterial == null && m_lightMaterial == null)
		{
			AutoFillMaterials();
		}

		Debug.Assert(m_darkMaterial != null, "PIPE: Missing Dark Material ref");
		Debug.Assert(m_lightMaterial != null, "PIPE: Missing Light Material ref");

		FindAllMaterialRefs();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FindAllMaterialRefs()
	{
		m_darkMaterialRefs.Clear();
		m_lightMaterialRefs.Clear();

		Renderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

		foreach(Renderer r in renderers)
		{
			Material[] mats = r.sharedMaterials;
			for(int i = 0; i < mats.Length; i++)
			{
				if(mats[i] == m_darkMaterial)
				{
					m_darkMaterialRefs.Add(new Pair<Renderer, int>(r, i));
				}
				else if(mats[i] == m_lightMaterial)
				{
					m_lightMaterialRefs.Add(new Pair<Renderer, int>(r, i));
				}
				else
				{
					Debug.LogWarningFormat("PIPE: Unknown material '{0}' did not match dark '{1}' or light '{2}'", 
						mats[i].name, m_darkMaterial.name, m_lightMaterial.name);
				}
			}
		}
	}

	public void AutoFillMaterials()
	{
		m_darkMaterial = null;
		m_lightMaterial = null;

		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers)
		{
			foreach(Material m in r.sharedMaterials)
			{
				if(m_darkMaterial == null && m != null && m.name.ToLower().Contains("dark"))
				{
					m_darkMaterial = m;
				}
				else if(m_lightMaterial == null && m != null && m.name.ToLower().Contains("light"))
				{
					m_lightMaterial = m;
				}
			}
		}
	}
	public void SetMaterialsFromPipe(Pipe pipe)
	{
		SetMaterials(pipe.m_darkMaterial, pipe.m_lightMaterial);
	}

	public void SetMaterials(Material darkMat, Material lightMat)
	{
		FindAllMaterialRefs();
		SetDarkMaterial(darkMat);
		SetLightMaterial(lightMat);
	}

	private void SetDarkMaterial(Material mat)
	{
		m_darkMaterial = mat;
		foreach(var p in m_darkMaterialRefs)
		{
			Renderer r = p.first;
			int index = p.second;
			var mats = r.sharedMaterials;
			mats[index] = mat;
			r.sharedMaterials = mats;
		}
	}

	private void SetLightMaterial(Material mat)
	{
		m_lightMaterial = mat;
		foreach(var p in m_lightMaterialRefs)
		{
			Renderer r = p.first;
			int index = p.second;
			var mats = r.sharedMaterials;
			mats[index] = mat;
			r.sharedMaterials = mats;
			// Debug.LogFormat("Renderer {0} index {1} mat {2} after {3}", r, index, mat.name, r.sharedMaterials[index].name);
		}
	}

}
