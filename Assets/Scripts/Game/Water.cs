using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Water : MonoBehaviour
{
    [SerializeField] private float m_BuoyancyForce;
    [SerializeField] private float m_MaxWaterLevel;
    [SerializeField] private float m_MinWaterLevel;
    [SerializeField] [Range(0,1)] private float m_WaterLevel;
    [SerializeField] Transform m_WaterTransform;

    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private BoxCollider waterCollider;

    private void Start()
    {
        waterCollider = transform.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        m_WaterTransform.localScale = new Vector3(m_WaterTransform.localScale.x, Mathf.MoveTowards(m_MinWaterLevel, m_MaxWaterLevel, m_WaterLevel * (m_MaxWaterLevel - m_MinWaterLevel)), m_WaterTransform.localScale.z);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {            
            rigidbodies[i].AddForce(Vector3.up * m_BuoyancyForce * Mathf.Clamp01(m_WaterTransform.position.y + waterCollider.transform.lossyScale.y - rigidbodies[i].transform.position.y), ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = 1;
            rb.angularDrag = 1;
            rigidbodies.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null) return;

        if (rigidbodies.Contains(rigidbody))
        {
            rigidbody.drag = 0;
            rigidbody.angularDrag = 0.05f;
            rigidbodies.Remove(rigidbody);
        }
        
    }

    public void AddWater(float waterFillingSpeed)
    {
        m_WaterLevel = Mathf.MoveTowards(m_WaterLevel, m_MaxWaterLevel, waterFillingSpeed * Time.deltaTime);
    }

    public void RemoveWater(float waterEvaporationSpeed)
    {
        m_WaterLevel = Mathf.MoveTowards(m_WaterLevel, 0f, waterEvaporationSpeed * Time.deltaTime);
    }
}
