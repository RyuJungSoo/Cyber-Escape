using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapComponent : MonoBehaviour
{
    // Start is called before the first frame update
    
    SpriteRenderer renderer;
    BoxCollider2D boxCollider;

    public Sprite[] spikeTrapSprites;

    GameObject Player;
    ObstacleComponent obstacleComponent;


    int length = 0;
    float changeTimer = 0f;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        obstacleComponent = GetComponent<ObstacleComponent>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer += Time.deltaTime;
        //0.2초마다 SpriteRender의 sprite를 바꿈

        //첫번째 프레임일때 데미지 안들어가게 
        obstacleComponent.isNotAttacking = length > 1 ? true : false;
        if (changeTimer >= 0.3f)
        {
            //바꾸는 줄
            renderer.sprite = spikeTrapSprites[length];

            /*if (length == 0 && Mathf.Abs(transform.position.x - Player.transform.position.x) < 2)
            {
                SoundManager.Instance.AudioPlay(EnumSpace.SoundType.SPIKETRAP);
            }*/

            //콜라이더 사이즈를 sprite 사이즈만큼 늘림
            boxCollider.size = renderer.sprite.bounds.size;
            //collider 위치를 맞추기 위해서 offset을 sprite 가운데로 맞춤
            boxCollider.offset = new Vector2(0, spikeTrapSprites[length].bounds.center.y);
            //다음 이미지로 넘어가기 위해서 length를 증가
            length++;
            changeTimer = 0;
        }

        //다시 초기화 
        if (length == 4)
            length = 0;
    }
}
