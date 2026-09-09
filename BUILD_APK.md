# Grape Tag VR - Build Instructions

## APK Build Guide

To build an APK file for Meta Quest, follow these steps:

### Prerequisites
1. Unity 2022.3.15f1 installed
2. Android SDK installed (via Unity Hub)
3. Meta Quest SDK for Unity imported
4. Photon PUN2 imported from Asset Store

### Building the APK

1. **Switch to Android Platform**
   - File → Build Settings
   - Select Android platform
   - Click "Switch Platform"

2. **Configure Player Settings**
   - Edit → Project Settings → Player
   - Set Company Name: "GrapeTag"
   - Set Product Name: "GrapeTagVR"
   - Set Minimum API Level: 29
   - Set Target API Level: 33

3. **Configure XR Settings**
   - Edit → Project Settings → XR Plug-in Management
   - Enable "Oculus" for Android
   - Select Quest 2/3 as target device

4. **Build APK**
   - File → Build Settings
   - Click "Build" or "Build and Run"
   - Choose output folder (e.g., `Builds/`)
   - Unity will generate `GrapeTagVR.apk`

5. **Deploy to Meta Quest**
   - Connect Meta Quest via USB cable
   - Enable Developer Mode on headset
   - Run: `adb install GrapeTagVR.apk`
   - Or use "Build and Run" in Unity

### APK File Size
- Uncompressed: ~500MB - 1GB
- Compressed: ~200-300MB

### Testing the Build
1. Put on your Meta Quest headset
2. Launch GrapeTag VR from Apps Library
3. Allow microphone and hand tracking permissions
4. Start a hangout session and invite friends!

### Troubleshooting Build Issues

**Build fails with "No Android SDK found"**
- Go to Edit → Preferences → External Tools
- Set Android SDK Path to your SDK location

**APK crashes on launch**
- Check Logcat: `adb logcat | grep GrapeTag`
- Ensure all permissions are granted in AndroidManifest.xml

**Hand tracking not working**
- Verify Oculus plugin is enabled
- Check OVRProjectConfig.asset has hand tracking enabled

### Cloud Deployment

To make the APK available online:

1. **GitHub Releases**
   - Go to Repository → Releases
   - Create a new release
   - Upload APK file
   - Add changelog and instructions

2. **Alternative Hosting**
   - Upload to itch.io (free VR game hosting)
   - Upload to SideQuest (Meta Quest app platform)
   - Host on your own website

---

**Next Steps:**
- Build the APK following the steps above
- Test on your Meta Quest device
- Share with friends via GitHub Releases or your preferred platform
