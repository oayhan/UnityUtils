using UnityEngine;

namespace UnityUtils
{
    /// <summary>
    /// Finds and caches components on this GameObject, on demand.
    /// </summary>
    public class LazyLoadBehaviour : MonoBehaviour
    {
        #region Transforms

        private Transform transformComponent;

        public Transform Transform
        {
            get { return transformComponent ? transformComponent : (transformComponent = GetComponent<Transform>()); }
        }

        private RectTransform rectTransformComponent;

        public RectTransform RectTransform
        {
            get
            {
                return rectTransformComponent
                    ? rectTransformComponent
                    : (rectTransformComponent = GetComponent<RectTransform>());
            }
        }

        #endregion

        #region Rigidbodies

        private Rigidbody rigidbodyComponent;

        public Rigidbody Rigidbody
        {
            get { return rigidbodyComponent ? rigidbodyComponent : (rigidbodyComponent = GetComponent<Rigidbody>()); }
        }

        private Rigidbody2D rigidbody2DComponent;

        public Rigidbody2D Rigidbody2D
        {
            get
            {
                return rigidbody2DComponent
                    ? rigidbody2DComponent
                    : (rigidbody2DComponent = GetComponent<Rigidbody2D>());
            }
        }


        #endregion

        #region 3D Colliders

        private Collider colliderComponent;

        public Collider Collider
        {
            get { return colliderComponent ? colliderComponent : (colliderComponent = GetComponent<Collider>()); }
        }

        private SphereCollider sphereCollider;

        public SphereCollider SphereCollider
        {
            get { return sphereCollider ? sphereCollider : (sphereCollider = GetComponent<SphereCollider>()); }
        }

        private CapsuleCollider capsuleCollider;

        public CapsuleCollider CapsuleCollider
        {
            get { return capsuleCollider ? capsuleCollider : (capsuleCollider = GetComponent<CapsuleCollider>()); }
        }

        private BoxCollider boxCollider;

        public BoxCollider BoxCollider
        {
            get { return boxCollider ? boxCollider : (boxCollider = GetComponent<BoxCollider>()); }
        }

        private MeshCollider meshCollider;

        public MeshCollider MeshCollider
        {
            get { return meshCollider ? meshCollider : (meshCollider = GetComponent<MeshCollider>()); }
        }

        #endregion

        #region 2D Colliders

        private Collider2D collider2DComponent;

        public Collider2D Collider2D
        {
            get
            {
                return collider2DComponent ? collider2DComponent : (collider2DComponent = GetComponent<Collider2D>());
            }
        }

        private BoxCollider2D boxCollider2DComponent;

        public BoxCollider2D BoxCollider2D
        {
            get
            {
                return boxCollider2DComponent
                    ? boxCollider2DComponent
                    : (boxCollider2DComponent = GetComponent<BoxCollider2D>());
            }
        }

        private CapsuleCollider2D capsuleCollider2DComponent;

        public CapsuleCollider2D CapsuleCollider2D
        {
            get
            {
                return capsuleCollider2DComponent
                    ? capsuleCollider2DComponent
                    : (capsuleCollider2DComponent = GetComponent<CapsuleCollider2D>());
            }
        }

        private CircleCollider2D circleCollider2DComponent;

        public CircleCollider2D CircleCollider2D
        {
            get
            {
                return circleCollider2DComponent
                    ? circleCollider2DComponent
                    : (circleCollider2DComponent = GetComponent<CircleCollider2D>());
            }
        }

        private EdgeCollider2D edgeCollider2DComponent;

        public EdgeCollider2D EdgeCollider2D
        {
            get
            {
                return edgeCollider2DComponent
                    ? edgeCollider2DComponent
                    : (edgeCollider2DComponent = GetComponent<EdgeCollider2D>());
            }
        }

        #endregion

        #region Animation

        private Animation animationComponent;

        public Animation AnimationComponent
        {
            get { return animationComponent ? animationComponent : (animationComponent = GetComponent<Animation>()); }
        }

        private Animator animatorComponent;

        public Animator Animator
        {
            get { return animatorComponent ? animatorComponent : (animatorComponent = GetComponent<Animator>()); }
        }

        #endregion

        #region Misc

        private AudioSource audioComponent;

        public AudioSource Audio
        {
            get { return audioComponent ? audioComponent : (audioComponent = GetComponent<AudioSource>()); }
        }

        private Renderer rendererComponent;

        public Renderer Renderer
        {
            get { return rendererComponent ? rendererComponent : (rendererComponent = GetComponent<Renderer>()); }
        }

        private Light lightComponent;

        public Light Light
        {
            get { return lightComponent ? lightComponent : (lightComponent = GetComponent<Light>()); }
        }

        private Camera cameraComponent;

        public Camera Camera
        {
            get { return cameraComponent ? cameraComponent : (cameraComponent = GetComponent<Camera>()); }
        }

        private ParticleSystem particleSystemComponent;

        public ParticleSystem ParticleSystem
        {
            get
            {
                return particleSystemComponent
                    ? particleSystemComponent
                    : (particleSystemComponent = GetComponent<ParticleSystem>());
            }
        }

        #endregion
    }
}
