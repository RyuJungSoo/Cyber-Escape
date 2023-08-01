using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapComponent : MonoBehaviour
{
    // Start is called before the first frame update
    
    SpriteRenderer renderer;
    BoxCollider2D boxCollider;

    public Sprite[] spikeTrapSprites;


    ObstacleComponent obstacleComponent;


    int length = 0;
    float changeTimer = 0f;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        obstacleComponent = GetComponent<ObstacleComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer += Time.deltaTime;
        //0.2�ʸ��� SpriteRender�� sprite�� �ٲ�

        //ù��° �������϶� ������ �ȵ��� 
        obstacleComponent.isNotAttacking = length == 0 ? true : false;
        if (changeTimer >= 0.3f)
        {
            //�ٲٴ� ��
            renderer.sprite = spikeTrapSprites[length];
            //�ݶ��̴� ����� sprite �����ŭ �ø�
            boxCollider.size = renderer.sprite.bounds.size;
            //collider ��ġ�� ���߱� ���ؼ� offset�� sprite ����� ����
            boxCollider.offset = new Vector2(0, spikeTrapSprites[length].bounds.center.y);
            //���� �̹����� �Ѿ�� ���ؼ� length�� ����
            length++;
            changeTimer = 0;
        }

        //�ٽ� �ʱ�ȭ 
        if (length == 4)
            length = 0;
    }
}
