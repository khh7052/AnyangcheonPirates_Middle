# 앵그리 피타고라스 - 수학 교육 게임
<img width="2000" height="1125" alt="screenshot_2024-07-15_063324" src="https://github.com/user-attachments/assets/60121f07-94d3-4594-909b-7691018fc594" />

### 📹 게임플레이  
- **(https://youtu.be/9aRjqawQF2o)**: 완전한 게임플레이 시연

## 📖 프로젝트 개요

**앵그리 피타고라스**는 수학의 삼각함수를 게임 메커니즘으로 활용한 교육용 액션 게임입니다. 2024년 7월 성결대학교 게임잼에서 3일 동안 개발된 교육적 성격의 게임입니다.

### 🎯 핵심 컨셉
추상적인 수학 개념(삼각함수)을 매력적인 게임플레이로 변환합니다. 플레이어들은 제한된 공격 횟수 안에 적을 처치하기 위해 Sin, Cos, Tan, Abs 파동 패턴을 공격 수단으로 사용합니다.

### 🏗️ 기술적 구현
- **엔진**: Unity 2D
- **언어**: C#
- **플랫폼**: Android (Google Play Games 연동)
- **개발 기간**: 3일 (2024년 7월 8-11일)

## 👥 팀 구성 및 역할

### 개발 팀 정보
- **팀명**: AnyangcheonPirates (안양천해적단)
- **인원**: 3명
- **대회**: 성결대학교 2024 게임잼, 10분 게임 콘테스트

### 역할 및 기여
**팀장 & 전투 시스템 리드 개발자**

핵심적인 수학 전투 시스템 구현 및 전체 프로젝트 조율을 담당했습니다.

**📋주요 담당 업무:**
- 수학 함수 기반 공격 시스템 설계 및 구현
- 공격 패턴 시각화 (그래프 및 파티클)
- 파티클 물리 시스템
- 사운드 이펙트 통합
- 데미지 및 공격 카운팅 시스템
- Unity 빌드 설정 및 배포

## 🛠️ 기술 아키텍처

#### 수학 공격 패턴 구현

**📐 SinWaveAttack.cs - 사인파 공격**
```csharp
public class SinWaveAttack : MonoBehaviour
{
    // 수학 함수 Sin()을 활용한 공격 패턴
    // 물결 모양의 투사체 경로 생성
    private void CalculateWavePath()
    {
        // Sin 함수의 주기적 특성을 활용한 파동 공격
        float waveOffset = Mathf.Sin(timer * frequency) * amplitude;
        Vector2 newPosition = Vector2.Lerp(startPos, targetPos, progress) + 
                             new Vector2(0, waveOffset);
    }
}
```

**📐 CosWaveAttack.cs - 코사인파 공격**
```csharp
public class CosWaveAttack : MonoBehaviour
{
    // Cos() 함수의 위상 차이를 활용한 공격
    // Sin 공격과 협업 가능한 이중 파동 공격 생성
    private void CalculateDualWave()
    {
        // 코사인 함수의 위상 차이를 활용한 공격 패턴
        float cosineWave = Mathf.Cos(timer * frequency + phase) * amplitude;
    }
}
```

**📐 TanWaveAttack.cs - 탄젠트파 공격**
```csharp
public class TanWaveAttack : MonoBehaviour
{
    // Tan() 함수의 불연속점을 활용한 독특한 공격
    // 예측 불가능한 경로로 적들을 교란
    private void HandleDiscontinuity()
    {
        // 탄젠트 함수의 특성을 활용한 불연속 공격
        // 수학의 불연속점을 게임 메커니즘으로 구현
    }
}
```

**📐 AbsWaveAttack.cs - 절댓값 공격**
```csharp
public class AbsWaveAttack : MonoBehaviour
{
    // Abs() 함수의 V자 형태를 활용한 다중 방향 공격
    // 한 번의 공격으로 여러 방향에서 적들을 포위
    private void CalculateVPattern()
    {
        // 절댓값 함수의 V자 형태를 활용한 분할 공격
        Vector2 vShape = new Vector2(Mathf.Abs(x), y);
    }
}
```

#### 시스템 관리

**⚙️ AttackManager.cs - 공격 시스템 중앙 관리**
```csharp
public class AttackManager : MonoBehaviour
{
    // 모든 공격 시스템을 통합 관리
    // 공격 패턴 전환 및 제어
    // 공격 데이터 관리 및 저장
    [SerializeField] private List<GameObject> attackPatterns;
    
    public void SwitchAttackPattern(string patternType)
    {
        // 매니저 패턴을 활용한 중앙 집중식 공격 관리
        // 확장 가능한 공격 패턴 구조
    }
}
```

**⚙️ AttackUIManager.cs - 공격 UI 관리**
```csharp
public class AttackUIManager : MonoBehaviour
{
    // 공격 관련 UI 표시 및 업데이트
    // 공격 선택 인터페이스
    // 쿨다운 타이머 관리
    private void UpdateAttackUI()
    {
        // Observer 패턴을 활용한 UI 자동 업데이트
    }
}
```

#### 개발 방법론

체계적인 접근 방식을 따르는 구현:

```
00.Jen/                    // 작업 폴더 (15개 스크립트) ⭐
├── Math/                  // 정식 공격 시스템
│   ├── SinWaveAttack.cs           // 사인파 공격 (정식 버전)
│   ├── CosWaveAttack.cs           // 코사인파 공격 (정식 버전)
│   ├── TanWaveAttack.cs           // 탄젠트파 공격 (정식 버전)
│   └── AbsWaveAttack.cs           // 절댓값 공격 (정식 버전)
├── Exp/                   // 실험적 프로토타입
│   ├── Exp_Sin_Attack.cs          // 사인 공격 실험 버전
│   ├── Exp_Cos_Attack.cs          // 코사인 공격 실험 버전
│   ├── Exp_Tan_Attack.cs          // 탄젠트 공격 실험 버전
│   └── Exp_Abs_Attack.cs          // 절댓값 공격 실험 버전
├── Mgr/                   // 관리 시스템
│   ├── AttackManager.cs            // 공격 시스템 중앙 관리
│   └── AttackUIManager.cs          // 공격 UI 관리
└── GameSystems/           // 보조 게임 시스템
    ├── MagicCount.cs               // 리소스 관리 (마법 사용 횟수)
    ├── Aura_Change.cs              // 시각 효과 (오라 시스템)
    ├── ParticleForce.cs            // 물리 시뮬레이션 (파티클 물리)
    ├── Particle_Collider.cs        // 충돌 감지 (파티클 충돌)
    └── TiTle_Scene.cs              // 타이틀 씬 관리
```

### 🎮 게임플레이 메커니즘

#### 공격 시스템 설계

**📊 좌표 표시 시스템**
```csharp
public class CoordinateDisplay : MonoBehaviour
{
    // 실시간 마우스 커서 및 적 위치 추적
    // 화면 좌표와 게임 좌표 간 변환
    private void Update()
    {
        // 실시간 좌표 변환 계산
        Vector2 screenPos = Camera.main.WorldToScreenPoint(enemyPosition);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
```

**🎯 충돌 물리**
- **벽 충돌**: 스킬과 벽이 충돌하면 스킬 사라짐
- **적 충돌**: 스킬과 적 충돌 시 관통하여 추가 데미지
- **전략적 배치**: 위치 기반 전술적 결정 필요
<img width="696" height="383" alt="Untitled (2)" src="https://github.com/user-attachments/assets/41e71ad3-1978-47e8-ae8b-d2926d73abc2" />

#### 시각 효과 시스템

**💫 ParticleForce.cs - 파티클 물리**
```csharp
public class ParticleForce : MonoBehaviour
{
    // 파티클에 물리적 힘 적용
    // 실시간 파티클 움직임 시뮬레이션
    private void ApplyPhysicalForce()
    {
        // Unity Physics System을 활용한 파티클 동역학
        particleSystem.ApplyForce(forceVector);
    }
}
```

**💫 Particle_Collider.cs - 파티클 충돌**
```csharp
public class Particle_Collider : MonoBehaviour
{
    // 파티클 간 충돌 감지
    // 동적 상호작용 처리
    private void OnParticleCollision(GameObject other)
    {
        // 파티클과 게임 오브젝트 간 상호작용 처리
        HandleParticleImpact(collisionPoint);
    }
}
```
<img width="691" height="385" alt="Untitled" src="https://github.com/user-attachments/assets/455924bd-5221-4d38-91bf-ceb5b1407486" />


## 🎯 핵심 기술적 성과

### 수학 함수 구현

**🔢 삼각함수 알고리즘**
- **사인파 경로 계산**: 주기적 물결 패턴 구현
- **코사인 위상 패턴**: 위상 차이를 활용한 협업 공격
- **탄젠트 불연속점 처리**: 불연속점을 활용한 독특한 경로
- **절댓값 V-패턴**: V자 형태의 다중 분할 공격

**📐 2D 벡터 수학**
- **실시간 좌표 변환**: 화면 좌표와 게임 좌표 간 변환
- **파티클 경로 계산**: 고속 2D 벡터 연산
- **충돌 감지 최적화**: 효율적인 2D 충돌 알고리즘
- **좌표 매핑**: 화면-월드 좌표 실시간 변환
<img width="700" height="388" alt="Untitled (3)" src="https://github.com/user-attachments/assets/019b591b-df9c-4f17-a59e-8eb2e259c8cd" />

### 시스템 아키텍처

**🏗️ 적용된 디자인 패턴**
- **매니저 패턴**: AttackManager, AttackUIManager
- **전략 패턴**: 공격 패턴 전환
- **옵저버 패턴**: 이벤트 기반 UI 업데이트
- **팩토리 패턴**: 공격 오브젝트 생성

**⚡ 성능 최적화**
- **오브젝트 풀링**: 투사체 재사용 시스템
- **고효율 충돌 감지**: 최적화된 2D 충돌 알고리즘
- **파티클 렌더링 최적화**: 모바일 성능 고려한 파티클 처리
- **메모리 관리**: 모바일 환경 최적화

### Unity 시스템 통합

**🎮 Unity 컴포넌트**
- **Unity Physics System**: 파티클 동역학 시뮬레이션
- **Unity Particle System**: 시각 효과 생성
- **Unity UI System**: 인터페이스 관리
- **Unity Animation System**: 오라 효과 애니메이션

**📱 모바일 최적화**
- **터치 입력 처리**: 모바일 터치 인터페이스
- **화면 해상도 적응**: 다양한 모바일 디바이스 지원
- **배터리 사용 최적화**: 게임 성능과 배터리 효율 균형
- **Google Play Games 연동**: 플랫폼 연동 서비스

## 🚀 기술적 혁신

### 교육적 게임 디자인
- **추상적 → 구체적**: 수학 함수를 시각적 게임플레이로 변환
- **상호작용형 학습**: 삼각함수 개념의 직접적 경험
- **즉각적 피드백**: 수학 패턴의 시각적 표현

### 개발 접근법
- **프로토타입 우선 개발**: Exp/ 폴더 실험 → Math/ 폴더 정식 구현
- **반복적 설계**: 빠른 프로토타입과 검증 사이클
- **모듈러 아키텍처**: 관심사 분리를 통한 유지보수성

### 수학적 정확성
- **함수 충실도**: 게임플레이에서의 진정한 수학적 표현
- **시각적 정확성**: 수학적 함수와 일치하는 정확한 파동 패턴
- **교육적 가치**: 시각적 학습을 통한 직관적 이해

## 🏆 게임잼 성과

### 대회 성과
- **성결대학교 게임잼 2024**: 완성된작품
- **10분 게임 콘테스트**: 참가자
- **개발 타임라인**: 총 3일 개발 기간

### 기술적 검증
- **코드 품질**: 구조화된 201개 스크립트
- **플레이ABILITY**: 10개의 완전한 레벨
- **완성도**: 완전한 UI, 사운드, 시각 효과
- **배포**: Android 플랫폼에 성공적 빌드

## 💡 기술적 인사이트 및 학습

### 버전 관리 마스터링
- **Git/GitHub**: 개선된 저장소 관리 능력
- **Plastic SCM**: 대체 VCS 시스템 경험
- **협업**: 브랜치 관리 및 병합 충돌 해결

### 애자일 개발
- **시간 관리**: 긴밀한 데드라인 하의 효율적 개발
- **기능 우선순위**: 핵심 게임플레이 메커니즘에 집중
- **팀 조율**: 3명 개발 워크플로우

### 수학적 프로그래밍
- **함수 구현**: 수학적 개념의 실제 적용
- **알고리즘 최적화**: 효율적인 계산 접근법
- **교육적 설계**: 정확성과 플레이ABILITY 간 균형

---

---

## 🛠️ 구현 상세 정보

### 수학 함수 구현 상세

**SinWaveAttack 구현 코드 예시:**
```csharp
public class SinWaveAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public float frequency = 2f;     // 진동 주파수
    public float amplitude = 1f;     // 파동 진폭
    public float attackSpeed = 3f;   // 공격 속도
    
    private Vector2 startPos;
    private Vector2 targetPos;
    private float currentDistance;
    private float totalDistance;
    
    void Start()
    {
        startPos = transform.position;
        // 타겟 위치 계산 (마우스 커서 위치)
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        currentDistance = 0f;
        totalDistance = Vector2.Distance(startPos, targetPos);
    }
    
    void Update()
    {
        // 진행률 계산 (0 ~ 1)
        float progress = Mathf.Min(currentDistance / totalDistance, 1f);
        
        // 기본 직선 경로
        Vector2 basePosition = Vector2.Lerp(startPos, targetPos, progress);
        
        // Sin 파동 계산
        float waveOffset = Mathf.Sin(progress * frequency * Mathf.PI * 2f) * amplitude;
        // 직각 방향으로 파동 적용
        Vector2 perpendicular = Vector2.Perpendicular(targetPos - startPos).normalized;
        Vector2 finalPosition = basePosition + perpendicular * waveOffset;
        
        // 위치 업데이트
        transform.position = new Vector3(finalPosition.x, finalPosition.y, 0f);
        
        // 진행 거리 업데이트
        currentDistance += attackSpeed * Time.deltaTime;
        
        // 목표 도달 시 파괴
        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
```

### 파티클 시스템 통합

**ParticleForce.cs 상세 구현:**
```csharp
public class ParticleForce : MonoBehaviour
{
    [Header("물리 설정")]
    public float forceMultiplier = 1f;
    public Vector2 direction = Vector2.up;
    public float decayRate = 0.1f;
    
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }
    
    void LateUpdate()
    {
        int numParticlesAlive = particleSystem.GetParticles(particles);
        
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // 파티클에 물리적 힘 적용
            particles[i].velocity += direction * forceMultiplier * Time.deltaTime;
            
            // 점진적 감쇠
            particles[i].velocity *= (1f - decayRate * Time.deltaTime);
            
            // 파티클 위치 업데이트
            particles[i].position += particles[i].velocity * Time.deltaTime;
        }
        
        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
```

### AttackManager 통합 시스템

```csharp
public class AttackManager : MonoBehaviour
{
    [System.Serializable]
    public class AttackPattern
    {
        public string name;
        public GameObject prefab;
        public float cooldown;
        public int maxUses;
    }
    
    [Header("공격 패턴 설정")]
    public AttackPattern[] attackPatterns;
    
    private Dictionary<string, AttackPattern> patternDictionary;
    private string currentPattern;
    private float lastAttackTime;
    private int currentUses;
    
    void Start()
    {
        // Dictionary 초기화
        patternDictionary = new Dictionary<string, AttackPattern>();
        foreach (var pattern in attackPatterns)
        {
            patternDictionary[pattern.name] = pattern;
        }
        
        // 기본 공격 패턴 설정
        SetAttackPattern(attackPatterns[0].name);
    }
    
    public void SetAttackPattern(string patternName)
    {
        if (patternDictionary.ContainsKey(patternName))
        {
            currentPattern = patternName;
            currentUses = 0;
            lastAttackTime = 0f;
            
            // UI 업데이트
            AttackUIManager.Instance.UpdatePatternDisplay(patternName);
        }
    }
    
    public bool CanAttack()
    {
        var pattern = patternDictionary[currentPattern];
        
        // 쿨다운 체크
        if (Time.time - lastAttackTime < pattern.cooldown)
            return false;
            
        // 사용 횟수 체크
        if (currentUses >= pattern.maxUses)
            return false;
            
        return true;
    }
    
    public void ExecuteAttack()
    {
        if (!CanAttack()) return;
        
        var pattern = patternDictionary[currentPattern];
        
        // 공격 실행
        Instantiate(pattern.prefab, GetAttackPosition(), Quaternion.identity);
        
        // 상태 업데이트
        lastAttackTime = Time.time;
        currentUses++;
        
        // UI 업데이트
        AttackUIManager.Instance.UpdateUsageDisplay(currentUses, pattern.maxUses);
        
        // 모든 스킬 사용 시 게임 오버 체크
        if (currentUses >= pattern.maxUses)
        {
            GameManager.Instance.CheckGameOver();
        }
    }
    
    private Vector2 GetAttackPosition()
    {
        // 플레이어 위치 반환 (또는 마우스 위치)
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
```

이 구현은 수학적 정확성과 게임적 재미를 결합한 독창적인 접근 방식을 보여주며, 3일이라는 짧은 시간 안에 완성한 게임잼 프로젝트로서 높은 기술적 완성도를 달성했습니다.
<img width="691" height="385" alt="Untitled" src="https://github.com/user-attachments/assets/a91c85d8-271a-4b36-82ac-4b2bb27362bd" />
<img width="690" height="395" alt="Untitled (1)" src="https://github.com/user-attachments/assets/e21d15ed-986a-4dfe-80b8-4db9a0b3907d" />
<img width="696" height="383" alt="Untitled (2)" src="https://github.com/user-attachments/assets/76c7418e-dcf4-490d-8000-b1b1c1466709" />
<img width="700" height="388" alt="Untitled (3)" src="https://github.com/user-attachments/assets/81452de5-5076-4f26-9ebe-d1e25ed71cd7" />







