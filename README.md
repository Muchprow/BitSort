# 🌀 BitSort v2.2

A lightweight, powerful, and safe open-source utility designed for developers and power users to purge bloated system junk, deep IT development caches, and redundant third-party hardware driver packages.

---

## 🚀 Key Features

BitSort splits clutter into three precise categories so you only wipe what you actually want:

### 1. 💻 Deep Developer Cache Purge
Modern dev environments leave gigabytes of duplicate files. BitSort targets:
* **Java/Android:** `.gradle` local build and dependency caches.
* **.NET/C#:** Global `.nuget` package storage.
* **JavaScript:** Node.js `npm-cache` logs and global `Yarn` package installer caches.
* **Python:** Downloader source tarballs and `.whl` files from `pip`.
* **Rust:** Local crate registry data (`.cargo`).
* **Go / Docker / C++:** Temporary build artifacts (`gocache`, `vcpkg`, Docker daemon logs).

### 2. 🗑️ Smart System Cleanup
* Clears environment and shared OS **Temp directories** (`User Temp` & `Windows Temp`).
* Safely scans and targets dead system error reports, `.log` files, and `.bak` backups.

### 3. 🎮 Native Hardware Driver Management
* Uses native Windows PnP API (`pnputil`) to safely scan and detach orphaned or legacy display/audio driver packages (AMD/NVIDIA).
* **Safe Architecture:** Includes automatic hardware detection. If you only run on CPU graphics, it guarantees your integrated architecture remains untouched.

---

## 🛠️ How to Download & Run

Since BitSort is compiled as a standalone **Single-File Native Executable**, you don't need to install any .NET runtime environments.

1. Go to the [Releases](https://github.com/Muchprow/BitSort/releases) section.
2. Download `BitSortApp.exe`.
3. Right-click the file -> **Run as Administrator** *(required for Windows PnP driver integration & System Temp access)*.
4. Select your targets and click **▶ START OPTIMIZATION**.

Note: Run the application as Administrator to allow deep cache cleaning and driver scanning.

---

## 📸 Screenshots & UI

The application features a modern, eye-friendly dark professional suite containing a real-time execution log console, live activity tracking, and exact item descriptions.

*Check the "About App" passport button inside the window to verify internal binary metadata credentials!*

---

## 🏗️ Technical Details

* **Core Engine:** C# / .NET 8.0
* **GUI Framework:** Windows Forms (High-DPI Optimized)
* **Architecture:** Standalone `win-x64` Native AOT/JIT Compilation
* **Security:** 100% transparent open-source code. Zero telemetry. Zero background services.

---

## ⚖️ License

This project is licensed under the GNU License - see the [LICENSE](LICENSE) file for details.

Developed with 💻 by [Muchprow](https://github.com/Muchprow).    
