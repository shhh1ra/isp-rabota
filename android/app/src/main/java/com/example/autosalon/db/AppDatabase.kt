package com.example.autosalon.db

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase

@Database(
    entities = [
        ManufacturerEntity::class,
        BrandEntity::class,
        CarEntity::class
    ],
    version = 1
)
abstract class AppDatabase : RoomDatabase() {

    abstract fun carDao(): CarDao

    companion object {
        private var INSTANCE: AppDatabase? = null

        fun get(context: Context): AppDatabase {
            if (INSTANCE == null) {
                INSTANCE = Room.databaseBuilder(
                    context.applicationContext,
                    AppDatabase::class.java,
                    "autosalon.db"
                )
                    .allowMainThreadQueries() // для учебного проекта
                    .build()
            }
            return INSTANCE!!
        }
    }
}
