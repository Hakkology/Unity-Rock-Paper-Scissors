[**🇹🇷 TR**](#tr) | [**🇺🇸 EN**](#en)

<a id="en"></a>
## EN

# Rock Paper Scissors: Arena Z

Rock Paper Scissors: Arena Z is a real-time autonomous simulation built with Unity (C#), focusing on interface-driven architecture, type-safe design, and LINQ-powered queries over entity collections.

---

## Game Overview

Entities representing Rocks, Papers, and Scissors spawn on two opposing sides and interact based on classic dominance rules. Each entity operates independently, and the match resolves automatically based on team composition and unit types.

---

## Core Features

- **Interface-Based Entity Design**: `IRock`, `IPaper`, and `IScissor` interfaces layered over a shared `Entity` class provide type-safe behavior and clean polymorphism.  
- **LINQ-Driven Architecture**: All game logic—resolution checks, team analysis, and UI updates—is powered by LINQ queries on the `AllEntities` list.  
- **Zone-Based Arena**: Left and right sides have independent bounds, spawn points, and resolution logic.  
- **Conversion System**: Entities convert defeated opponents into their own type, triggering visual and audio feedback.  
- **Queued Conversion Logic**: All type changes are handled through a centralized queue, processed sequentially to prevent conflicts during overlapping interactions.  
- **Shader Feedback**:  
  - `ArenaBackground` displays live type ratios with soft animated transitions.  
  - `FrameOutline` adds pulsing outlines and gradient motion to UI panels.  
- **Modular Management**: `Arena`, `GameManager`, `HUDManager`, and `SoundManager` are fully decoupled and scoped to specific responsibilities.

---

## Game Mechanics

- Entities spawn in triangular formations per side.  
- Each entity moves autonomously and bounces within arena bounds.  
- On contact, if type dominance applies, the target is added to the conversion queue.  
- Queue is processed sequentially to instantiate a new object of the converting type.  
- When both sides resolve to a single type or a side dominates, the match ends.

---

## Development

- **Engine & Language**: Unity (C#)  
- **Focus Areas**:  
  - Interface segregation  
  - LINQ-based entity queries  
  - Centralized queue handling  
  - Visual feedback via custom shaders  

---

## Access

Play the game online (WebGL) at:  
https://webbysoftinit.com/games/arenaz

---

<a id="tr"></a>
## TR

# Rock Paper Scissors: Arena Z

Rock Paper Scissors: Arena Z, Unity (C#) ile geliştirilmiş, arayüz tabanlı mimari, tür güvenli tasarım ve LINQ destekli varlık sorgularını öne çıkaran gerçek zamanlı bir otonom simülasyon oyunudur.

---

## Oyun Genel Bakış

Taş, kağıt ve makas birimleri arenanın iki tarafında doğar ve klasik üstünlük kurallarına göre birbirini etkiler. Her birim bağımsız çalışır ve oyun, taraf kompozisyonuna göre otomatik olarak çözülür.

---

## Temel Özellikler

- **Arayüz Tabanlı Varlık Tasarımı**: `Entity` sınıfı üzerine tanımlı `IRock`, `IPaper` ve `IScissor` arayüzleriyle tür bazlı davranışlar güvenli ve okunaklı şekilde yönetilir.  
- **LINQ Destekli Mimari**: Tüm oyun mantığı—çözümleme, tür sayımı ve UI güncellemeleri—`AllEntities` listesi üzerinden LINQ ile gerçekleştirilir.  
- **Bölge Bazlı Arena Yapısı**: Sol ve sağ taraflar kendi sınırlarına, doğma noktalarına ve çözümleme mantığına sahiptir.  
- **Dönüşüm Sistemi**: Birimler, üstün geldikleri rakiplerini kendi türlerine dönüştürür. Görsel ve işitsel geri bildirimler bu dönüşümle birlikte tetiklenir.  
- **Kuyruk Tabanlı Dönüşüm Sistemi**: Tüm tür dönüşümleri merkezi bir kuyrukta toplanır ve sırayla işlenir. Bu yapı, eş zamanlı çarpışmalarda çakışmaları engeller ve stabil bir dönüşüm süreci sağlar.  
- **Shader Tabanlı Geri Bildirim**:  
  - `ArenaBackground` shader’ı, tür oranlarını animasyonlu geçişlerle yansıtır.  
  - `FrameOutline` shader’ı, UI panellerine nabız ve gradyan animasyonları kazandırır.  
- **Modüler Yönetici Yapılar**: `Arena`, `GameManager`, `HUDManager`, `SoundManager` gibi yöneticiler görev bazlı ayrılmıştır ve bağımsız şekilde çalışır.

---

## Oynanış Mekaniği

- Her iki tarafta birimler üçgen düzende doğar.  
- Birimler arenada sekerek bağımsız şekilde hareket eder.  
- Temas anında üstün gelen tür, hedefi dönüşüm kuyruğuna ekler.  
- Kuyruk sıralı şekilde işlenerek yeni tür birim oluşturulur.  
- Her iki taraf tek türe düştüğünde veya bir taraf üstünlük sağladığında oyun sonlanır.

---

## Geliştirme

- **Motor & Dil**: Unity (C#)  
- **Odak Noktaları**:  
  - Arayüz ayrımı  
  - LINQ ile mantık ve UI kontrolü  
  - Merkezi kuyruk yönetimi  
  - Shader ile görsel geri bildirim  

---

## Erişim

WebGL üzerinden oyunu şu adresten oynayabilirsiniz:  
https://webbysoftinit.com/games/arenaz
