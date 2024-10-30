# PlatformerProject3D
 
## 구현기능
- **기본 이동 및 점프** 
- **체력바 UI** 
- **동적 환경 조사** 
- **점프대** 
- **아이템 데이터** 
 
## 트러블슈팅
<details>
<summary>카메라 시점이 계속 움직이는 문제</summary>
<div markdown="1">

 ![해당사진](https://ifh.cc/g/DHpDjy.gif)

- 문제상황:

 마우스의 이동이 끝남과 동시에 시점이동도 끝나야합니다. 하지만 마우스를 멈췄는데도 시점이 계속 이동하는 현상 발생
 가끔씩이지만 이동하면서 시점이동이 튀기도 합니다.

- 해결과정

 1. 처음에 Look이벤트를 등록할때 started로 등록해서 발생한 문제라 생각해서 performed로 코드를 수정
 2. 여전히 해당 문제는 해결되지 않음. InputSystem에서 mousedelta값의 잔류문제인가 싶어 CameraLook()메서드 마지막에
    mouseDelta의 벡터값을 초기화하는 로직을 추가해서 해당 문제 해결
</div>
</details>

<details>
<summary>점프대 콜라이더에 연속해서 부딪힐 때, AddForce가 연속해서 작동하지 않는 문제</summary>
<div markdown="1">

- 문제상황:

 점프대 콜라이더와 부딪히고 OnCollisionEnter메서드가 실행되고나서 착지를 그라운드가 아니라 점프대에 착지하면
 기획상 연속해서 AddForce를 받고 도약을 해야하는데 그렇지 않는 상황이 발생

- 해결과정

 1. bool형식의 IsJumping를 false로 초기화 한 뒤, AddForce를 받는 조건문에 !IsJumping을 추가. 그리고 AddForce를 받고난 뒤 IsJumping을 true로 초기화
    그리고나서 OnCollisionExit메서드를 작성해 IsJumping을 false로 초기화

    -> 점프대콜라이더와의 충돌여부를 확실하게 구별하고나면 연속충돌문제를 해결할 수 있다고 기대
    -> 여전히 해결이 안됨.
2. 여러 검색 끝에 Rigidbody의 IsKinematic을 true로 변경하고 게임을 진행해봄
   콜라이더에 연속충돌하면 OnCollisionEnter가 연속으로 작동함. 하지만 AddForce가 중첩되는 문제? 플레이어 캐릭터의 벡터속도값이 중첩되는 문제가 발생.
   점프대에 뛰면 뛸수록 더 높이 뛰게 됨(거의 무한의 높이까지 올라감)
3. 2번 문제를 해결하기위해 OnCollisionEnter메서드에서 항상 맨처음에 벡터 수직 속도값이 0이 되도록 초기화를 진행
   ->기획,의도한 대로 점프대가 작동 
   
</div>
</details>
