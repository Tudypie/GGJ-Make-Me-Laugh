using UnityEngine;
using System.Collections;

namespace GameJam.Utilities.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialSwitcher : MonoBehaviour
    {
        [SerializeField] private float switchInterval;
        [Space]
        [SerializeField] private Material originalMaterial;
        [SerializeField] private Material materialToSwitchTo;

        private MeshRenderer meshRenderer;

        void Awake() 
        {
            meshRenderer = GetComponent<MeshRenderer>();

            StartCoroutine(SwitchMaterials());
        }

        private IEnumerator SwitchMaterials()
        {
            while (true)
            {
                yield return new WaitForSeconds(switchInterval);

                meshRenderer.material = materialToSwitchTo;

                yield return new WaitForSeconds(switchInterval);

                meshRenderer.material = originalMaterial;
            }
        }
    }
}
