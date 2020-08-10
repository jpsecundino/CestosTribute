using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject shadow;
    public Shader ballShader;
    public Shader shadowShader;
    public bool isDissolving;
    public bool isConsolidating;

    [Range(0f,10f)]
    public float dissolveSpeed = 0.04f;
    [Range(0f,10f)]
    public float consolidateSpeed = 0.04f;


    
    private Material ballMaterial;
    private Material shadowMaterial; 
    
    private float dissolvingEffectFade;
    

    private void Awake() {
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        Renderer shadowRenderer = shadow.GetComponent<Renderer>();   

        ballMaterial = new Material(ballShader);
        shadowMaterial = new Material(shadowShader);

        ballRenderer.material = ballMaterial;
        shadowRenderer.material = shadowMaterial;
    }
    void Start()
    {
        isDissolving = false;
        isConsolidating = true;
        dissolvingEffectFade = 0;
    }

    void Update() 
    {
        if(isConsolidating)
        {
            ConsolidateRoutine();
        } else if(isDissolving)
        {
            DissolveRoutine();
        }    
    }

    public void DissolveRoutine()
    {
        dissolvingEffectFade -= dissolveSpeed * Time.deltaTime;

            if(dissolvingEffectFade <= 0)
            {
                dissolvingEffectFade = 0f;
                isDissolving = false;
            }

            shadowMaterial.SetFloat("_Fade", dissolvingEffectFade);
            ballMaterial.SetFloat("_Fade", dissolvingEffectFade);
    }

    public void ConsolidateRoutine(){
            
            dissolvingEffectFade += consolidateSpeed * Time.deltaTime;

            if(dissolvingEffectFade >= 1)
            {
                dissolvingEffectFade = 1f;
                isConsolidating = false;
            }

            shadowMaterial.SetFloat("_Fade", dissolvingEffectFade);
            ballMaterial.SetFloat("_Fade", dissolvingEffectFade);

    }

    public void Dissolve(){
        isDissolving = true;
    }

    public void Consolidate(){
        isConsolidating = true;
    }
}
