using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour 
{

    [System.Serializable]
    public class Animation
    {
        public string Name = "";
        public List<Vector2> Frames = new List<Vector2>();
        public bool Loop;
        public float FrameDelay = 4f; //set this once
        private float frameCounter = 0f;

        public int CurrentFrameIndex = 0;
        public void NextFrame()
        {
            if (frameCounter > 0.001)
            {
                frameCounter--;
                return;
            }

            CurrentFrameIndex++;
            CurrentFrameIndex %= Frames.Count;
            frameCounter = FrameDelay;
        }
    }

    [SerializeField]
    public Vector2 NumberOfFrames;

    [SerializeField]
    public List<Animation> Animations = new List<Animation>();
    private Animation currentAnimation = null;

    [SerializeField]
    public string CurrentAnimationName = "";

    /// <summary>
    /// Scale of the texture. Set to 1/NumFrames
    /// </summary>
    private Vector2 textureScale;
    /// <summary>
    /// The mesh renderer of this object
    /// </summary>
    private MeshRenderer myRenderer;

    /// <summary>
    /// Sets the current animation to the specified name
    /// </summary>
    /// <param name="name"></param>
    public void SetCurrentAnimation(string name)
    {
        foreach (Animation ani in this.Animations)
        {
            if (ani.Name == name)
            {
                currentAnimation = ani;
                return;
            }
        }
        Debug.LogError(string.Format("Animation {0} not found!", name));
    }

	// Use this for initialization
	void Start () 
    {
        //Debug.Log(this.GetComponent<MeshRenderer>().material.mainTexture.width);
        myRenderer = this.GetComponent<MeshRenderer>();
        textureScale = new Vector2(1.0f / NumberOfFrames.x, 1.0f / NumberOfFrames.y);

        myRenderer.material.mainTextureScale = textureScale;

        SetCurrentAnimation(CurrentAnimationName);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void FixedUpdate()
    {
        try
        {
            if (currentAnimation == null)
                return;

            Vector2 currentFrameOffset = currentAnimation.Frames[currentAnimation.CurrentFrameIndex];
            myRenderer.material.mainTextureOffset = new Vector2(currentFrameOffset.x * textureScale.x, currentFrameOffset.y * textureScale.y);
            currentAnimation.NextFrame();
        }
        catch (UnityException ex)
        {
            Debug.LogError("Failed to fixed update animated sprite object: " + ex.Message);
        }
    }
}
