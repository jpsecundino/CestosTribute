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
    private Material ballMaterial;
    private Material shadowMaterial; 
    
    private float dissolvingEffectFade;
    
    public bool isDissolving;
    public bool isConsolidating;
    

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
        dissolvingEffectFade -= Time.deltaTime;

            if(dissolvingEffectFade <= 0)
            {
                dissolvingEffectFade = 0f;
                isDissolving = false;
            }

            shadowMaterial.SetFloat("_Fade", dissolvingEffectFade);
            ballMaterial.SetFloat("_Fade", dissolvingEffectFade);
    }

    public void ConsolidateRoutine(){
            
            dissolvingEffectFade += Time.deltaTime;

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
