plugins {
    id("com.android.application")
    id("org.jetbrains.kotlin.android")
    id("com.google.devtools.ksp")
}

android {
    namespace = "com.example.autosalon"
    //noinspection GradleDependency
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.autosalon"
        minSdk = 34
        //noinspection OldTargetApi
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"
        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildFeatures {
        viewBinding = true
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_17
        targetCompatibility = JavaVersion.VERSION_17
    }

    kotlinOptions {
        jvmTarget = "17"
    }
}

dependencies {
    val roomVersion = "2.6.1"

    //noinspection UseTomlInstead,GradleDependency
    implementation("androidx.core:core-ktx:1.13.1")
    //noinspection UseTomlInstead,GradleDependency
    implementation("androidx.appcompat:appcompat:1.7.0")
    //noinspection UseTomlInstead,GradleDependency
    implementation("com.google.android.material:material:1.13.0")
    //noinspection UseTomlInstead,GradleDependency
    implementation("androidx.constraintlayout:constraintlayout:2.2.1")

    //noinspection UseTomlInstead,GradleDependency
    implementation("androidx.room:room-runtime:$roomVersion")
    //noinspection UseTomlInstead,GradleDependency
    ksp("androidx.room:room-compiler:$roomVersion")
    //noinspection UseTomlInstead,GradleDependency
    implementation("androidx.recyclerview:recyclerview:1.3.2")
}