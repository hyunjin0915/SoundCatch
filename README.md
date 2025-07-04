### 🎓서울여자대학교 소프트웨어융합학과 졸업작품 [2024.04 ~ 2024.11]  
#### 🏆 스마트미디어학회 논문 우수상 수상
<br>

## 사용 기술 | Technologies Used
![](https://img.shields.io/badge/unity-%23000000.svg?logo=unity&logoColor=white)
![](https://img.shields.io/badge/-C%23-000000?logo=Csharp&style=flat) <br>
![OpenCV](https://img.shields.io/badge/opencv-%23white.svg?logo=opencv&logoColor=white)
![](https://img.shields.io/badge/NaverClova-pink)  
<img width="5%" src="https://techstack-generator.vercel.app/github-icon.svg"/>
<br> </br>
## 프로젝트 소개 | Project Introduction

### 시각장애인의 게임 접근성 향상을 위한 핸드트래킹 게임

시각장애인과 비시각장애인의 **동일한 게임 경험**을 목표로
OpenCV와 손동작 인식 인공지능, 유니티를 활용하여 핸드트래킹 기술과 음성 UI로 구성된 게임
<br> </br>

### 다양한 미니게임들
#### 각각 3개의 난이도를 가진 3가지 미니게임
<img width="481" alt="Image" src="https://github.com/user-attachments/assets/2f3c149f-8e56-477e-ac91-8d15f658b130" />

### 한국디지털접근성진흥원 방문 인터뷰 진행
- [한국디지털접근성진흥원](http://www.kwacc.or.kr/) 사무실에 직접 방문
- 프로토타입으로 제작한 게임으로 시각장애인 전맹, 시각장애인 저시력 전문가 분들의 플레이 경험 피드백을 받아 수정 및 개선

## 프로젝트 구조 | Project Structure
<img width="562" alt="Image" src="https://github.com/user-attachments/assets/0d713050-5801-4de5-908f-83cad419a7fa" />

## 클라이언트 개발을 맡은 파트 | Role
✔️ 클라이언트 개발 공동 작업 ✔️ 미니게임 - 숨은소리찾기 제작

### [ScriptableObject와 이벤트 구조를 사용한 안내음성 시스템 제작]
![Image](https://github.com/user-attachments/assets/cc911981-c36f-4c50-9541-0100aa0baaa5)

- 네이버 클로바를 사용하여 안내 음성 소스 제작
- ScriptableObject와 이벤트 구조를 사용하여 모듈형 디자인 구현 <br> 
  -> 음성 출력을 담당하는 클래스와, 출력 되는 음성 소스가 같은 씬에 존재하지 않아도 됨  
  -> 변경 사항 및 디버깅을 더 쉽게 관리 가능, 컴포넌트 재사용 용이
  
### [Scene Management]
**비동기 방식 & Additive Scene 모드 사용**  
<img width="464" alt="Image" src="https://github.com/user-attachments/assets/aa7e009f-2429-424a-92c4-f78abfdf290e" />
- **HandTracking Scene**에 게임 전체에 사용되는 것들을 두고 나머지 씬들을 **Additive Mode** 로 Load/ Unloade 해주는 방식으로 제작  
  -> 기능 별로 씬을 나누어 개발과 협업에 용이  
  -> 메모리의 효율적 사용 및 관리 용이

### [MiniGame - 숨은소리찾기 구현]
<img width="151" alt="Image" src="https://github.com/user-attachments/assets/4b18272f-8c39-4f3e-8bae-ba89286d061d" /> <br>   
손을 움직이며 블럭들 중 양쪽 귀에 들리는 소리가 다른 블록을 찾아 3초 간 주먹을 쥐는 게임







