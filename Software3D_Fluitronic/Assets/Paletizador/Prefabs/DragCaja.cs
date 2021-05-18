using UnityEngine;
using UnityEngine.Events;

namespace Assets.Paletizador.Prefabs
{
    public class DragCaja:MonoBehaviour
    {
        float zPostion;
        Vector3 offset;
        //Camera mainCamera;
        bool cogido;

        #region Inspector Variables
        [SerializeField]
        public Camera mainCamera;
        [Space]
        [SerializeField]
        public UnityEvent OnBeginDrag;
        [SerializeField]
        public UnityEvent OnEndDrag;
        #endregion

        private void Start()
        {
            //zPostion = mainCamera.WorldToScreenPoint(transform.position).z;
        }

        private void Update()
        {
            if (cogido)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPostion);
                transform.position = mainCamera.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
            }
            var rot = transform.rotation;
            rot.y += Time.deltaTime + 1.5f;
            transform.rotation = rot;
        }

        private void OnMouseDown()
        {
            if (!cogido)
            {
                BeginDrag();
            }
        }

        private void OnMouseUp()
        {
            EndDrag();
        }

        public void BeginDrag()
        {
            OnBeginDrag.Invoke();
            cogido = true;
            zPostion = mainCamera.WorldToScreenPoint(transform.position).z;
            offset = mainCamera.WorldToScreenPoint(transform.position) - Input.mousePosition;
        }

        public void EndDrag()
        {
            OnEndDrag.Invoke();
            cogido = false;
        }
    }
}
