package com.example.autosalon.ui

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.autosalon.databinding.ActivityLoginBinding
import com.example.autosalon.MainActivity

class LoginActivity : AppCompatActivity() {

    private lateinit var b: ActivityLoginBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        b = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(b.root)

        b.btnLogin.setOnClickListener {
            val login = b.etLogin.text.toString().trim()
            val pass = b.etPassword.text.toString().trim()

            if (login == "admin" && pass == "admin") {
                startActivity(Intent(this, MainActivity::class.java))
//                finish()
            } else {
                Toast.makeText(this, "Неверный логин или пароль", Toast.LENGTH_SHORT).show()
            }
        }
    }
}
