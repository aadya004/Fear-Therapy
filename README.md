# Fear Therapy — VR Exposure Therapy System

> *Blending psychology, technology, and empathy to support mental well-being through immersive virtual reality.*

A virtual reality application designed to help users confront and gradually overcome common phobias through controlled, progressive exposure therapy — personalised in real time using biometric feedback.

Built for **Meta Quest 2** using **Unity** and **C#**.

---

## Overview

Fear Therapy simulates real-world phobia triggers in a safe, controlled virtual environment. As the user engages with each scenario, their **real-time heart rate** is monitored via the ThingsSpeak API to dynamically adjust the intensity of the experience — ensuring it remains challenging but never overwhelming.

The system targets three phobias:
- **Acrophobia** — fear of heights
- **Nyctophobia** — fear of darkness
- **Claustrophobia** — fear of confined spaces

This project was built as part of an academic submission exploring the intersection of immersive technology and mental health — drawing on principles of **Cognitive Behavioural Therapy (CBT)** and **Graduated Exposure Therapy**.

---

## Features

- **Multi-Phobia Scenarios** — Distinct immersive 3D environments for each phobia, designed to feel realistic yet safe
- **Real-Time Biometric Adaptation** — Heart rate data pulled live from ThingsSpeak API dynamically adjusts scene intensity
- **Progressive Difficulty Scaling** — Exposure intensity increases gradually as the user grows more comfortable
- **Modular Scene Architecture** — Reusable Unity components for scalable scenario creation
- **Meta Quest 2 Optimised** — Performance-tuned for standalone VR hardware ensuring smooth, comfortable frame rates

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Game Engine | Unity |
| Scripting Language | C# |
| VR Hardware | Meta Quest 2 |
| Biometric Integration | ThingsSpeak API |
| 3D Design | Unity built-in tools, Blender |

---

## System Architecture

```
User Puts On Headset
        │
        ▼
  Phobia Selection Menu
        │
        ▼
  3D Environment Loads (Low Intensity)
        │
        ▼
  ThingsSpeak API ──► Live Heart Rate Feed
        │
        ▼
  Adaptive Difficulty Controller
        │
   ┌────┴────────┐
   ▼             ▼
Escalate      Hold / Reduce
Intensity     Intensity
```

---

## Getting Started

### Prerequisites
- Unity 2022.x or above
- Meta Quest 2 headset
- Android Build Support module installed in Unity
- ThingsSpeak account with a heart rate monitoring channel configured

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/fear-therapy.git
```

2. Open the project in Unity Hub

3. Switch build platform to **Android** (required for Meta Quest 2)
```
File → Build Settings → Android → Switch Platform
```

4. Add your ThingsSpeak API key in `HeartRateMonitor.cs`
```csharp
private string apiKey = "YOUR_THINGSPEAK_API_KEY";
private string channelId = "YOUR_CHANNEL_ID";
```

5. Build and deploy to Meta Quest 2
```
File → Build Settings → Build and Run
```

---

## How It Works

1. User selects a phobia scenario from the main menu
2. VR environment loads at its lowest intensity level
3. Heart rate is polled in real time from a wearable device via ThingsSpeak API
4. `AdaptiveDifficultyController` continuously evaluates heart rate against a threshold
5. If heart rate stays calm → intensity gradually increases
6. If heart rate spikes → intensity is held or reduced to avoid overwhelming the user
7. Session ends with a calming cooldown environment

---

## 📊 Performance Targets

| Metric | Target |
|---|---|
| Frame Rate | 72 FPS (Meta Quest 2 standard) |
| Heart Rate API Polling | < 2 second latency |
| Scene Load Time | < 3 seconds |

---

## Future Scope

- [ ] Therapist dashboard to monitor patient sessions remotely
- [ ] Persistent session history and progress tracking per user
- [ ] Additional phobia scenarios (arachnophobia, social anxiety)
- [ ] Integration with clinical-grade biometric wearables
- [ ] Voice-guided breathing exercises between exposure intervals

---

## Why This Project

Most therapy tools are screen-based and passive. VR changes that — it puts the person *inside* the experience, which is exactly what exposure therapy needs. Building Fear Therapy meant thinking about the user not just as someone clicking through an interface, but as someone who is genuinely anxious, genuinely brave, and deserving of a system that responds to them as a human being.

That's the design philosophy behind every decision here — from the adaptive difficulty to the biometric integration.

---

## Author

**Aadya, Dhruv Jha, Yuvika Mehta**
B.Tech Computer Science Engineering, SRM IST

---

## License

This project is for academic and personal use only. Not intended for clinical or medical deployment.
