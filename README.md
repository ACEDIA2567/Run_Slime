<div align="center">
<h1> Run_Slime </h1>
</div>

# 목차    
* [개요](#개요)
* [게임 설명](#게임-설명)
* [게임 플레이 방식](#게임-플레이-방식)

## 개요    
<div align="center">
<h3>RunSlime</h3>
슬라임 무리에서 떨어진 슬라임! 자신의 무리로 되돌아갈 수 있을까?
</div>

### ⚡ 빌드 파일
**● PC(exe) : **  

### ⚙️ 개발 환경
- 언어 : `C#`
- IDE : `Visual Studio 2020`
- 엔진 : `Unity 2020.3.31f1`

### 🕐 개발 기간
___22.03.02 ~ 24.06.07___

## 게임 설명
### 게임 
* 장르: 2D, Platformer(플랫포머)

|시작 화면|엔딩 화면|
|---|---|
|<img src=https://github.com/user-attachments/assets/88b8b3b6-11b7-4c79-a8af-a79f09ecc544 width="500" height="300">|<img src=https://github.com/user-attachments/assets/c50312cf-4ced-48c3-99e6-06f2a96c0adc width="500" height="300">|

* 📖 많은 도전를 해보자
  여러 아이템과 함정이 있어 한번에 클리어하기 힘듭니다.

* 🎲 다양한 아이템
  아이템을 사용해야 스테이지를 넘어갈 수 있습니다.
  아이템을 적극적으로 활용하세요.

* 💜 다양한 스테이지 
  스테이지를 진행할 수록 난이도가 점점 오르게됩니다.
  한 번의 실수로 처음으로 돌아 갈 수 있습니다.

</br>

## 게임 플레이 방식
### 🎮 조작 방법
- 이동기 점프(W), 좌우 이동키(A, D)를 이용하여 플레이어 자신을 이동시킨다.
- 아이템은 마우스 좌클릭과, 우클릭를 이용한다.
- Space키는 특수키로 이동하는 방향으로 짧게 대쉬를 한다.
- 여러 개의 장애물을 피하면서 여럿 문제를 풀어서 목표 지점으로 간다.

### 🏹 진행 방법
|열쇠 획득과 문 상호작용|
|---|
|<img src=https://user-images.githubusercontent.com/101154683/164979949-1f2c0bf6-2108-48a6-922b-65838579101e.gif width="500" height="300">|
* 열쇠: 스테이지마다 1개씩 존재하며 다음 스테이지의 문을 열 수 있게 해주는 아이템이다.
* 문: 스테이지 사이마다 있으며 플레이어가 열쇠를 가지고 있어야 상호작용이 가능하다.

### 💖 플레이어
|점프|대쉬|
|---|---|
|<img src=https://user-images.githubusercontent.com/101154683/164979927-4f15dda7-818e-4e11-a2f1-e90694ec944a.gif width="500" height="300">|<img src=https://user-images.githubusercontent.com/101154683/164979931-5bf9181d-4c65-4a6f-b58e-6107e3aa4239.gif width="500" height="300">|
* 점프: 플레이어가 땅에 있을 때 할 수 있으며 누른 시간에 따라서 낮은 점프, 높은 점프를 할 수 있다.
* 대쉬: 전방으로 빠르게 대쉬를 하여 먼 곳으로 이동할 수 있다.

### 🔨 장애물
|함정|이동 발판|물|
|---|---|---|
|<img src=https://user-images.githubusercontent.com/101154683/164979925-23d37d32-013c-4e0e-9331-f2a22506a405.gif width="500" height="300">|<img src=https://user-images.githubusercontent.com/101154683/174471507-ab001f03-b73f-490d-9622-0d331d2c6e69.gif width="500" height="300">|<img src=https://user-images.githubusercontent.com/101154683/174471567-b04a0c26-dfc2-4b7b-80c3-795cc22ea480.gif width="500" height="300">|
* 함정: 충돌 시 플레이어가 바닥에 착지하기 전까지 제어를 하지 못하고 랜덤한 위치로 날라가게 된다.
* 이동 발판: 좌우 또는 상하로 이동하며 플레이어가 이동할 수 있게 해준다.
* 물: 플레이어의 이동속도, 떨어지는 속도를 증가시켜 방해한다.

### 🎯 아이템 사용
|더블 점프|순간이동|갈고리|
|---|---|---|
|<img src=https://user-images.githubusercontent.com/101154683/164979933-406a522c-b98f-4e62-a44b-ed95a792a0bb.gif width="400" height="200">|<img src=https://user-images.githubusercontent.com/101154683/164979920-146ad286-091a-4440-b93d-1a157a109765.gif width="400" height="200">|<img src=https://user-images.githubusercontent.com/101154683/164983824-c52d0a21-ebfa-43c7-b2c6-1a91f2f0e9df.gif width="400" height="200">|
* 더블 점프: 획득 시 점프를 1번 더 가능하게 해준다. (플레이어가 땅에 착지하면 점프 횟수는 사라진다.)
* 순간이동: 상하좌우와 대각선을 포함하여 8방향으로 원하는 방향으로 순간이동을 할 수 있게 해준다.
* 갈고리: 갈고기를 발사하여 후크를 고정대에 

### UI
|획득 아이템 확인|옵션|
|---|---|
|<img src=https://user-images.githubusercontent.com/101154683/174471674-bf391a28-f633-4e7f-a7cd-394a2055f20b.gif width="500" height="300">|<img src=https://user-images.githubusercontent.com/101154683/174471137-e2bd5742-d0d0-4074-a048-11cd0720bc8f.gif width="500" height="300">|
* 빨간 원: 게임 종료
* 파란 화살표: 스테이지별로 재 시작
* 노란 엑스: 돌아가기

### 📑 스테이지
|튜토리얼 맵|
|---|
|![image](https://github.com/user-attachments/assets/c76a43a1-b75b-4f19-90d7-d6a8e0ff9a14)|

|스테이지 1|
|---|
|![image](https://user-images.githubusercontent.com/101154683/174473639-ef99502a-657f-4c70-b246-767bc4605b5a.png)|

|스테이지 2|
|---|
|![image](https://user-images.githubusercontent.com/101154683/174473660-189c2507-cd24-4924-b769-5ef4e129c9ea.png)|

|스테이지 3|
|---|
|![image](https://user-images.githubusercontent.com/101154683/174473658-505a26cc-b9ec-42aa-83c1-cfb32de186a0.png)|


</br>



