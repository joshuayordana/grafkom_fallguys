﻿using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using LearnOpenTK.Common;

namespace FallGuys
{
    class Window : GameWindow
    {
        List<Asset3d> objectList = new List<Asset3d>();
        List<Asset3d> balonudara = new List<Asset3d>();
        List<Asset3d> animasi_gelinding = new List<Asset3d>();
        List<Asset3d> bola_goyang = new List<Asset3d>();
        List<Asset3d> character = new List<Asset3d>();
        List<Asset3d> center_tangan = new List<Asset3d>();
        Camera _camera;

        //untuk tilt kamera
        bool _firstmove = true;
        Vector2 _lastPos;
        float totalTime;

        int counter = 1;
        int speed_goyang = 15;
        float speed = 1.1f;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //warna bg nya sky blue: rgb(135,206,235)
            //GL.ClearColor(0.529f, 0.807f, 0.921f, 1.0f);

            //deepsky blue: rgb(0,191,255)
            //GL.ClearColor(0, 0.749f, 1, 1.0f);

            //royal blue: rgb(65,105,225)
            GL.ClearColor(0.254f, 0.411f, 1, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            //camera
            _camera = new Camera(new Vector3(0, 1, 5), Size.X / Size.Y);

            //menangkap kursor
            CursorGrabbed = true;

            #region obstacle cube 2 (bawah) (17) (jangan pindah posisinya, karena di animasiin)

            //warna orange code: (255,165,0)
            var tiang = new Asset3d(new Vector3(1, 0.647f, 0));
            tiang.createCylinder(0.1f, 0.3f, 0, 0, 3.0f);
            tiang.translate(0, -0.2f, 0.3f);
            objectList.Add(tiang);

            //-1.5f, -0.2f, 0a
            //tambahin x ngubah atas bawah, tambahin y ngubah kiri kanan, tambahin z nya jadi dekat
            //saddlebrown rgb(139,69,19)
            var tongkat = new Asset3d(new Vector3(0.545f, 0.270f, 0.074f));
            tongkat.createCylinder(0.1f, 1.6f, 0.1f, 0, 3.0f);
            tongkat.rotate(Vector3.Zero, Vector3.UnitZ, 90);
            tongkat.translate(0, -0.1f, 0.3f);
            objectList.Add(tongkat);

            //=================OBSTACLE DONAT UNGU=======================
            //radmajor itu scale kanan kiri, radminor itu scale atas bawah
            //mediumpurple rgb(147,112,219)
            var donat1 = new Asset3d(new Vector3(0.576f, 0.439f, 0.858f));
            donat1.createTorus(-0.6f, -0.2f, 1.9f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat1);

            var donat2 = new Asset3d(new Vector3(0.576f, 0.439f, 0.858f));
            donat2.createTorus(-0.6f, -0.1f, 1.9f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat2);

            var donat3 = new Asset3d(new Vector3(0.576f, 0.439f, 0.858f));
            donat3.createTorus(-0.6f, 0, 1.9f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat3);

            //warna orange code: (255,165,0)
            var tongkat_donat123 = new Asset3d(new Vector3(1, 0.647f, 0));
            tongkat_donat123.createCylinder(0.04f, 0.2f, -0.6f, 0, 1.9f);
            objectList.Add(tongkat_donat123);

            var atap_donat123 = new Asset3d(new Vector3(1, 0.647f, 0));
            atap_donat123.createCylinder(0.12f, 0.025f, -0.6f, 0.08f, 1.9f);
            objectList.Add(atap_donat123);

            //=================OBSTACLE DONAT MERAH=======================
            //radmajor itu scale kanan kiri, radminor itu scale atas bawah
            //crimson rgb(220,20,60)
            var donat4 = new Asset3d(new Vector3(0.977f, 0.078f, 0.235f));
            donat4.createTorus(0.7f, -0.2f, 1.6f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat4);

            var donat5 = new Asset3d(new Vector3(0.977f, 0.078f, 0.235f));
            donat5.createTorus(0.7f, -0.1f, 1.6f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat5);

            var donat6 = new Asset3d(new Vector3(0.977f, 0.078f, 0.235f));
            donat6.createTorus(0.7f, 0, 1.6f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat6);

            //warna powderblue code: rgb(176,224,230)
            var tongkat_donat456 = new Asset3d(new Vector3(0.690f, 0.878f, 0.901f));
            tongkat_donat456.createCylinder(0.04f, 0.2f, 0.7f, 0, 1.6f);
            objectList.Add(tongkat_donat456);

            var atap_donat456 = new Asset3d(new Vector3(0.690f, 0.878f, 0.901f));
            atap_donat456.createCylinder(0.12f, 0.025f, 0.7f, 0.08f, 1.6f);
            objectList.Add(atap_donat456);

            //=================OBSTACLE DONAT CADET BLUE=======================
            //radmajor itu scale kanan kiri, radminor itu scale atas bawah
            //cadet blue rgb(95,158,160)
            var donat7 = new Asset3d(new Vector3(0.372f, 0.619f, 0.627f));
            donat7.createTorus(0.2f, -0.2f, 2.2f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat7);

            var donat8 = new Asset3d(new Vector3(0.372f, 0.619f, 0.627f));
            donat8.createTorus(0.2f, -0.1f, 2.2f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat8);

            var donat9 = new Asset3d(new Vector3(0.372f, 0.619f, 0.627f));
            donat9.createTorus(0.2f, 0, 2.2f, 0.1f, 0.07f, 50, 50);
            objectList.Add(donat9);

            //warna limegreen code: rgb(50,205,50)
            var tongkat_donat789 = new Asset3d(new Vector3(0.196f, 0.803f, 0.196f));
            tongkat_donat789.createCylinder(0.04f, 0.2f, 0.2f, 0, 2.2f);
            objectList.Add(tongkat_donat789);

            var atap_donat789 = new Asset3d(new Vector3(0.196f, 0.803f, 0.196f));
            atap_donat789.createCylinder(0.12f, 0.025f, 0.2f, 0.08f, 2.2f);
            objectList.Add(atap_donat789);

            #endregion 

            #region obstacle cube 1 (1) (tengah)

            //tambahin x ngubah atas bawah, tambahin y ngubah kiri kanan, tambahin z nya jadi dekat
            //saddlebrown rgb(139,69,19)
            var rolling = new Asset3d(new Vector3(0.545f, 0.270f, 0.074f));
            rolling.createCylinder(0.1f, 0.8f, 0.57f, 0, -1.3f);
            rolling.rotate(Vector3.Zero, Vector3.UnitZ, 90);
            animasi_gelinding.Add(rolling);

            //center buat objek meng gelinding
            var center_gelinding = new Asset3d(new Vector3(1, 0, 0));
            center_gelinding.createCuboid(0, -0.3f, 0, 0.1f); //sama dengan createBoxVertices
            objectList.Add(center_gelinding);

            #endregion

            #region obstacle cube 3 (atas)

            var tiang_finish_kiri = new Asset3d(new Vector3(1, 1, 1));
            tiang_finish_kiri.createCuboid(0, 0.5f, 0, 0.3f); //sama dengan createBoxVertices
            tiang_finish_kiri.scale(0.3f, 5, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            tiang_finish_kiri.translate(-0.94f, -1.4f, -2.7f);
            objectList.Add(tiang_finish_kiri);

            var tiang_finish_kanan = new Asset3d(new Vector3(1, 1, 1));
            tiang_finish_kanan.createCuboid(0, 0.5f, 0, 0.3f); //sama dengan createBoxVertices
            tiang_finish_kanan.scale(0.3f, 5, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            tiang_finish_kanan.translate(0.94f, -1.4f, -2.7f);
            objectList.Add(tiang_finish_kanan);

            var bendera_finish_atas = new Asset3d(new Vector3(1, 1, 1));
            bendera_finish_atas.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            bendera_finish_atas.scale(12.0f, 2.0f, 0.1f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            bendera_finish_atas.translate(0, 0.67f, -2.7f);
            objectList.Add(bendera_finish_atas);

            var bendera_finish_bawah = new Asset3d(new Vector3(0, 0, 0));
            bendera_finish_bawah.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            bendera_finish_bawah.scale(12.0f, 2.0f, 0.1f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            bendera_finish_bawah.translate(0, 0.37f, -2.7f);
            objectList.Add(bendera_finish_bawah);

            //====================BOLA GOYANG-GOYANG===================
            //benda mulai dari benda ke 23
            //orange rgb (255,153,51)
            var center_bola = new Asset3d(new Vector3(1, 0.6f, 0.2f));
            center_bola.createEllipsoid(0, 2.3f, -2, 0.15f, 0.1f, 0.1f, 100, 100);
            objectList.Add(center_bola);

            //purple rgb (178,102,255) (benda ke 24, index nya 23)
            var center_bola_tengah = new Asset3d(new Vector3(0.698f, 0.4f, 1));
            center_bola_tengah.createEllipsoid(0, 2.3f, -2, 0.095f, 0.08f, 0.12f, 100, 100);
            objectList.Add(center_bola_tengah);

            //posisi awal tongkat : bola atas : bola bawah
            //0, 1.7f, -2 : 0, 0.95f, -2 : 0, 1.75f, -2
            //purple rgb (178,102,255)
            var tongkat_bola = new Asset3d(new Vector3(0.698f, 0.4f, 1));
            tongkat_bola.createCylinder(0.05f, 1.2f, 0, 1.7f, -2);
            tongkat_bola.rotate(center_bola_tengah.objectCenter, Vector3.UnitZ, -45);
            bola_goyang.Add(tongkat_bola);

            var bola_atas = new Asset3d(new Vector3(0.698f, 0.4f, 1));
            bola_atas.createHalfEllipsoid(0.3f, 0.3f, 0.3f, 0, 0.95f, -2);
            bola_atas.rotate(center_bola_tengah.objectCenter, Vector3.UnitZ, -45);
            bola_goyang.Add(bola_atas);

            //lightcoral rgb(240,128,128)
            var bola_bawah = new Asset3d(new Vector3(0.941f, 0.501f, 0.501f));
            bola_bawah.createHalfEllipsoid(0.3f, 0.3f, 0.3f, 0, 1.75f, -2);
            bola_bawah.rotate(bola_atas.objectCenter, Vector3.UnitX, 180);
            bola_bawah.rotate(center_bola_tengah.objectCenter, Vector3.UnitZ, -45);
            bola_goyang.Add(bola_bawah);
            #endregion

            #region platform

            //warnanya violet code: rgb(238,130,238)
            var cube1 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f));
            cube1.createCuboidFlat(0, 0, 0, 0.5f); //sama dengan createBoxVertices
            objectList.Add(cube1);
            cube1.rotate(Vector3.Zero, Vector3.UnitZ, 15);

            //atas bawah warna nya clay?: rgb(255 218 192)
            //bawah
            var cube2 = new Asset3d(new Vector3(1, 0.854f, 0.752f));
            cube2.createCuboidFlat(-2.6f, -0.38f, 0, 0.5f); //sama dengan createBoxVertices
            cube2.scale(1.3f, 1, 1);
            objectList.Add(cube2);

            //ungu: (136,101,255)
            var cube2_ = new Asset3d(new Vector3(0.533f, 0.396f, 1));
            cube2_.createCuboidFlat(-16.1f, -0.985f, 0, 0.5f); //sama dengan createBoxVertices
            cube2_.scale(0.3f, 0.3f, 1);
            objectList.Add(cube2_);

            //atas
            var cube3 = new Asset3d(new Vector3(1, 0.854f, 0.752f));
            cube3.createCuboidFlat(2.9f, 0.38f, 0, 0.5f); //sama dengan createBoxVertices
            objectList.Add(cube3);

            //ungu: (136,101,255)
            var cube3_ = new Asset3d(new Vector3(0.533f, 0.396f, 1));
            cube3_.createCuboidFlat(6.7f, 1.6f, 0, 0.5f); //sama dengan createBoxVertices
            cube3_.scale(0.53f, 0.3f, 1);
            objectList.Add(cube3_);

            //rotasi platform
            cube1.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube2.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube2_.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube3.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube3_.rotate(Vector3.Zero, Vector3.UnitY, 90);

            #endregion

            #region pagar

            var pagar1 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar1.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar1.scale(13.3f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar1.translate(0, -0.38f, -4.375f);
            objectList.Add(pagar1);

            var pagar2 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar2.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar2.scale(20.0f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar2.translate(6.2f, -0.38f, 2.29f);
            pagar2.rotate(tiang.objectCenter, Vector3.UnitY, 90);
            objectList.Add(pagar2);

            var pagar3 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar3.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar3.scale(20.0f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar3.translate(6.2f, -0.38f, 4.29f);
            pagar3.rotate(tiang.objectCenter, Vector3.UnitY, 90);
            objectList.Add(pagar3);

            var pagar6 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar6.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar6.scale(26.0f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar6.translate(-0.08f, -1.18f, 2.29f);
            pagar6.rotate(tiang.objectCenter, Vector3.UnitY, 90);
            objectList.Add(pagar6);

            var pagar7 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar7.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar7.scale(26.0f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar7.translate(-0.08f, -1.18f, 4.29f);
            pagar7.rotate(tiang.objectCenter, Vector3.UnitY, 90);
            objectList.Add(pagar7);

            var pagar8 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f)); //violet
            pagar8.createCuboid(0, 0.5f, 0, 0.15f); //sama dengan createBoxVertices
            pagar8.scale(13.3f, 2.0f, 0.3f);
            //translate: x kanan kiri, y atas bawah, z jauh dekat
            pagar8.translate(0, -1.18f, 5.31f);
            objectList.Add(pagar8);

            #endregion

            #region awan

            var awan1 = new Asset3d(new Vector3(1, 1, 1));
            awan1.createEllipsoid(0, 1.0f, 0, 0.6f, 0.2f, 0.5f, 100, 100);
            objectList.Add(awan1);

            var awan2 = new Asset3d(new Vector3(1, 1, 1));
            awan2.createEllipsoid(-0.2f, 1.2f, 0, 0.3f, 0.2f, 0.3f, 100, 100);
            objectList.Add(awan2);

            var awan3 = new Asset3d(new Vector3(1, 1, 1));
            awan3.createEllipsoid(0.2f, 1.2f, 0, 0.3f, 0.3f, 0.3f, 100, 100);
            objectList.Add(awan3);

            awan1.translate(0.6f, 1.8f, 0);
            awan2.translate(0.6f, 1.8f, 0);
            awan3.translate(0.6f, 1.8f, 0);

            //===================AWAN 2============================
            var awan4 = new Asset3d(new Vector3(1, 1, 1));
            awan4.createEllipsoid(0.5f, 1.0f, 0, 0.6f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan4);

            var awan5 = new Asset3d(new Vector3(1, 1, 1));
            awan5.createEllipsoid(-0.1f, 1.0f, 0, 0.5f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan5);

            var awan6 = new Asset3d(new Vector3(1, 1, 1));
            awan6.createEllipsoid(0.2f, 1.3f, 0, 0.6f, 0.3f, 0.3f, 100, 100);
            objectList.Add(awan6);

            awan4.translate(-1.5f, 1.4f, 4.0f);
            awan5.translate(-1.5f, 1.4f, 4.0f);
            awan6.translate(-1.5f, 1.4f, 4.0f);

            //===================AWAN 3============================
            var awan7 = new Asset3d(new Vector3(1, 1, 1));
            awan7.createEllipsoid(-0.3f, 1.2f, 0, 0.5f, 0.6f, 0.5f, 100, 100);
            objectList.Add(awan7);

            var awan8 = new Asset3d(new Vector3(1, 1, 1));
            awan8.createEllipsoid(0.3f, 1.2f, 0, 0.3f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan8);

            var awan9 = new Asset3d(new Vector3(1, 1, 1));
            awan9.createEllipsoid(0, 0.9f, 0, 1.0f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan9);

            awan7.translate(2.0f, 1.6f, 3.5f);
            awan8.translate(2.0f, 1.6f, 3.5f);
            awan9.translate(2.0f, 1.6f, 3.5f);

            //===================AWAN 3 duplicate============================
            var awan10 = new Asset3d(new Vector3(1, 1, 1));
            awan10.createEllipsoid(-0.3f, 1.0f, 0, 0.5f, 0.6f, 0.5f, 100, 100);
            objectList.Add(awan10);

            var awan11 = new Asset3d(new Vector3(1, 1, 1));
            awan11.createEllipsoid(0.3f, 1.0f, 0, 0.3f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan11);

            var awan12 = new Asset3d(new Vector3(1, 1, 1));
            awan12.createEllipsoid(0, 0.7f, 0, 1.0f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan12);

            awan10.translate(-2.2f, 2.0f, 1.0f);
            awan11.translate(-2.2f, 2.0f, 1.0f);
            awan12.translate(-2.2f, 2.0f, 1.0f);

            //===================AWAN 2 DUPLICATE============================
            var awan13 = new Asset3d(new Vector3(1, 1, 1));
            awan13.createEllipsoid(0.5f, 1.0f, 0, 0.6f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan13);

            var awan14 = new Asset3d(new Vector3(1, 1, 1));
            awan14.createEllipsoid(-0.1f, 1.0f, 0, 0.5f, 0.3f, 0.5f, 100, 100);
            objectList.Add(awan14);

            var awan15 = new Asset3d(new Vector3(1, 1, 1));
            awan15.createEllipsoid(0.2f, 1.3f, 0, 0.6f, 0.3f, 0.3f, 100, 100);
            objectList.Add(awan15);

            awan13.translate(-1.5f, 2.1f, -5.0f);
            awan14.translate(-1.5f, 2.1f, -5.0f);
            awan15.translate(-1.5f, 2.1f, -5.0f);

            //===================AWAN 1 duplicate============================
            var awan16 = new Asset3d(new Vector3(1, 1, 1));
            awan16.createEllipsoid(0, 1.0f, 0, 0.6f, 0.2f, 0.5f, 100, 100);
            objectList.Add(awan16);

            var awan17 = new Asset3d(new Vector3(1, 1, 1));
            awan17.createEllipsoid(-0.2f, 1.2f, 0, 0.3f, 0.2f, 0.3f, 100, 100);
            objectList.Add(awan17);

            var awan18 = new Asset3d(new Vector3(1, 1, 1));
            awan18.createEllipsoid(0.2f, 1.2f, 0, 0.3f, 0.3f, 0.3f, 100, 100);
            objectList.Add(awan18);

            awan16.translate(1.5f, 1.9f, -4.5f);
            awan17.translate(1.5f, 1.9f, -4.5f);
            awan18.translate(1.5f, 1.9f, -4.5f);

            #endregion

            #region balon udara

            //Green = (9, 165, 92) = (0.035f, 0.647f, 0.361f)
            var balon = new Asset3d(new Vector3(0.035f, 0.647f, 0.361f));
            balon.createEllipsoid(0f, 2f, 0f, 1f, 0.5f, 0.5f, 100, 100);
            balon.translate(0, -0.2f, 0.7f);
            balonudara.Add(balon);

            var siripbelakang = new Asset3d(new Vector3(0.8f, 0f, 0f));
            siripbelakang.createCuboid(1f, 2f, 0f, 0.2f);
            siripbelakang.scale(1f, 0.1f, 2.5f);
            siripbelakang.translate(-0.05f, 1.6f, 0.7f);
            balonudara.Add(siripbelakang);

            var siripbelakang2 = new Asset3d(new Vector3(0.8f, 0f, 0f));
            siripbelakang2.createCuboid(1f, 2f, 0f, 0.2f);
            siripbelakang2.scale(1f, 2.5f, 0.1f);
            siripbelakang2.translate(-0.05f, -3.225f, 0.7f);
            balonudara.Add(siripbelakang2);

            var siripbawah = new Asset3d(new Vector3(0.8f, 0f, 0f));
            siripbawah.createCuboid(1f, 2f, 0f, 0.2f);
            siripbawah.scale(3f, 2.5f, 1f);
            siripbawah.translate(-3f, -3.65f, 0.7f);
            balonudara.Add(siripbawah);

            var jendela = new Asset3d(new Vector3(0f, 0f, 0f));
            jendela.createCuboid(1f, 2f, 0f, 0.2f);
            jendela.scale(0.5f, 0.5f, 1.2f);
            jendela.translate(-0.78f, 0.2f, 0.7f);
            balonudara.Add(jendela);

            var jendela2 = new Asset3d(new Vector3(0f, 0f, 0f));
            jendela2.createCuboid(1f, 2f, 0f, 0.2f);
            jendela2.scale(1f, 0.5f, 1.2f);
            jendela2.translate(-1.1f, 0.2f, 0.7f);
            balonudara.Add(jendela2);

            var jendela3 = new Asset3d(new Vector3(0f, 0f, 0f));
            jendela3.createCuboid(1f, 2f, 0f, 0.2f);
            jendela3.scale(1f, 0.5f, 1.2f);
            jendela3.translate(-0.855f, 0.2f, 0.7f);
            balonudara.Add(jendela3);

            var sumberAsap = new Asset3d(new Vector3(0f, 0f, 1f));
            sumberAsap.createEllipsoid(1.2f, 1.77f, 0.71f, 0f, 0f, 0f, 10, 10);
            balonudara.Add(sumberAsap);

            #endregion

            #region character

            #region badan
            var badanTabung = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            badanTabung.createCylinder(0.15f, 0.25f, 0.0f, 0.0f, 0.0f);
            badanTabung.translate(0.0f, 0.1f, 4.8f);
            character.Add(badanTabung);

            var centerLompat = new Asset3d(new Vector3(1, 0, 0));
            centerLompat.createEllipsoid(0.0f, 0.1f, 4.8f, 0.01f, 0.01f, 0.01f, 100, 100);
            center_tangan.Add(centerLompat);

            var badanSetengahLinkgaranAtas = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            badanSetengahLinkgaranAtas.createHalfEllipsoid(0.15f, 0.15f, 0.15f, 0.0f, 0.0f, 0.0f);
            badanSetengahLinkgaranAtas.translate(0.0f, 0.225f, 4.8f);
            character.Add(badanSetengahLinkgaranAtas);

            var badanSetengahLingkaranBawah = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            badanSetengahLingkaranBawah.createHalfEllipsoid(0.15f, 0.15f, 0.15f, 0.0f, 0.0f, 0.0f);
            badanSetengahLingkaranBawah.translate(0.0f, 0.0225f, 4.8f);
            badanSetengahLingkaranBawah.rotate(Vector3.Zero, Vector3.UnitZ, 180);
            character.Add(badanSetengahLingkaranBawah);
            #endregion

            #region tangan (objek ke 4 dan 5, index 3 dan 4)
            var tanganKanan = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            tanganKanan.createEllipsoid(0.0f, 0.0f, 0.0f, 0.2f, 0.0375f, 0.0375f, 100, 100);
            tanganKanan.translate(0.0f, 0.18f, 4.8f);
            tanganKanan.rotate(Vector3.Zero, Vector3.UnitZ, 75f);
            character.Add(tanganKanan);

            var tanganKiri = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            tanganKiri.createEllipsoid(0.0f, 0.0f, 0.0f, 0.2f, 0.0375f, 0.0375f, 100, 100);
            tanganKiri.translate(0.0f, 0.18f, 4.8f);
            tanganKiri.rotate(Vector3.Zero, Vector3.UnitZ, -75f);
            character.Add(tanganKiri);
            #endregion

            #region topi cone

            var cone = new Asset3d(new Vector3(1.0f, 0.67f, 0.0f));
            cone.createEllipticCone(0.05f, 0.05f, 0.15f, 0.0f, 0.0f, 0.0f, 100, 100);
            cone.translate(0.0f, 4.8f, -0.6f);
            cone.rotate(Vector3.Zero, Vector3.UnitX, 90f);
            character.Add(cone);

            var kotakCone = new Asset3d(new Vector3(1.0f, 0.67f, 0.0f));
            kotakCone.createBox(0.0f, 0.365f, 4.8f, 0.165f);
            //kotakCone.translate(0.0f, 0.365f, 4.8f);
            character.Add(kotakCone);

            #endregion

            #region muka
            var muka = new Asset3d(new Vector3(1.0f, 1.0f, 1.0f));
            muka.createEllipsoid(0.0f, 0.0f, 0.0f, 0.0875f, 0.075f, 0.05f, 100, 100);
            muka.translate(0.0f, 0.2f, 4.66f);
            character.Add(muka);

            #endregion

            #region mata

            #region Mata Kanan
            var tabungMataKanan = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            tabungMataKanan.createCylinder(0.0125f, 0.025f, 0.0f, 0.0f, 0.0f);
            tabungMataKanan.translate(0.03f, 0.209f, 4.62f);
            character.Add(tabungMataKanan);

            var setengahLingkaranAtasKanan = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            setengahLingkaranAtasKanan.createHalfEllipsoid(0.0125f, 0.015f, 0.01252f, -0.07f, 0.22f, 0.33f);
            setengahLingkaranAtasKanan.translate(0.1f, 0.0f, 4.29f);
            character.Add(setengahLingkaranAtasKanan);

            var setengahLingkaranBawahKanan = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            setengahLingkaranBawahKanan.createHalfEllipsoid(0.0125f, 0.015f, 0.01252f, -0.07f, 0.22f, 0.33f);
            setengahLingkaranBawahKanan.translate(0.04f, -0.417f, 4.29f);
            setengahLingkaranBawahKanan.rotate(Vector3.Zero, Vector3.UnitZ, 180);
            character.Add(setengahLingkaranBawahKanan);

            #endregion

            #region Mata Kiri
            var tabungMataKiri = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            tabungMataKiri.createCylinder(0.0125f, 0.025f, 0.0f, 0.0f, 0.0f);
            tabungMataKiri.translate(-0.03f, 0.209f, 4.62f);
            character.Add(tabungMataKiri);

            var setengahLingkaranAtasKiri = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            setengahLingkaranAtasKiri.createHalfEllipsoid(0.0125f, 0.015f, 0.01252f, -0.07f, 0.22f, 0.33f);
            setengahLingkaranAtasKiri.translate(0.04f, 0.0f, 4.29f);
            character.Add(setengahLingkaranAtasKiri);

            var setengahLingkaranBawahKiri = new Asset3d(new Vector3(0.0f, 0.0f, 0.0f));
            setengahLingkaranBawahKiri.createHalfEllipsoid(0.0125f, 0.015f, 0.01252f, -0.07f, 0.22f, 0.33f);
            setengahLingkaranBawahKiri.translate(0.1f, -0.417f, 4.29f);
            setengahLingkaranBawahKiri.rotate(Vector3.Zero, Vector3.UnitZ, 180);
            character.Add(setengahLingkaranBawahKiri);

            #endregion

            #endregion

            #region kaki

            var kakiKanan = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            kakiKanan.createCylinder(0.03f, 0.1f, 0.0f, 0.0f, 0.0f);
            kakiKanan.translate(0.05f, -0.2f, 4.8f);
            character.Add(kakiKanan);

            var kakiKiri = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            kakiKiri.createCylinder(0.03f, 0.1f, 0.0f, 0.0f, 0.0f);
            kakiKiri.translate(-0.05f, -0.2f, 4.8f);
            character.Add(kakiKiri);

            #endregion

            #region sepatu
            var sepatuKanan = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            sepatuKanan.createHalfEllipsoid(0.0325f, 0.115f, 0.0175f, 0.0f, 0.0f, 0.0f);
            sepatuKanan.translate(0.05f, -4.83f, -0.25f);
            sepatuKanan.rotate(Vector3.Zero, Vector3.UnitX, -90f);
            character.Add(sepatuKanan);

            var sepatuKiri = new Asset3d(new Vector3(0.99f, 0.20f, 0.53f));
            sepatuKiri.createHalfEllipsoid(0.0325f, 0.115f, 0.0175f, 0.0f, 0.0f, 0.0f);
            sepatuKiri.translate(-0.05f, -4.83f, -0.25f);
            sepatuKiri.rotate(Vector3.Zero, Vector3.UnitX, -90f);
            character.Add(sepatuKiri);
            #endregion

            #region celana

            var celana = new Asset3d(new Vector3(1.0f, 0.67f, 0.0f));
            celana.createHalfEllipsoid(0.16f, 0.16f, 0.15f, 0.0f, 0.0f, 0.0f);
            celana.translate(0.0f, 0.0225f, 4.8f);
            celana.rotate(Vector3.Zero, Vector3.UnitZ, 180);
            character.Add(celana);

            var sabuk = new Asset3d(new Vector3(1.0f, 1.0f, 1.0f));
            sabuk.createCylinder(0.16f, 0.027f, 0.0f, 0.0f, 0.0f);
            sabuk.translate(0.0f, -0.02f, 4.8f);
            character.Add(sabuk);

            #endregion

            #endregion

            foreach (Asset3d i in objectList)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in balonudara)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in animasi_gelinding)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bola_goyang)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in character)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in center_tangan)
            {
                i.load(Size.X, Size.Y);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            float time = (float)args.Time; //Deltatime ==> waktu antara frame sebelumnya ke frame berikutnya, gunakan untuk animasi
            totalTime = totalTime + time;
            float time_char = (float)args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // DepthBufferBit juga harus di clear karena kita memakai depth testing.

            foreach (Asset3d i in objectList)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            objectList.ElementAt(1).rotate(objectList.ElementAt(0).objectCenter, Vector3.UnitY, 10 * time);
            //objectList.ElementAt(17).translate(1.0f, 0, -1.0f);

            // RENDER BALON UDARA
            foreach (Asset3d i in balonudara)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                i.rotate(objectList.ElementAt(0).objectCenter, Vector3.UnitY, 5 * time);
            }

            foreach (Asset3d i in center_tangan)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            //ANIMASI LOMPAT CHARACTER
            //Y: 0.60256356
            //if (character.ElementAt(1).objectCenter.Y == 0.60256356f || character.ElementAt(1).objectCenter.Y == 0.22651537f)
            //{
            //    time_char *= -1;
            //}
            foreach (Asset3d i in character)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                //i.translate(0, 0.1f * time_char, 0);
                //character.ElementAt(3).rotate(center_tangan.ElementAt(1).objectCenter, Vector3.UnitY, 10 * time);

                //Console.WriteLine("Y: " + character.ElementAt(1).objectCenter.Y);
            }

            foreach (Asset3d i in bola_goyang)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());

                i.rotate(objectList.ElementAt(23).objectCenter, Vector3.UnitZ, speed_goyang * time);
                //Console.WriteLine("X " + bola_goyang.ElementAt(2).objectCenter.X);
            }

            //animasi gelinding 
            //posisi awal: Y 0.56831574 Z - 1.2936841
            //posisi akhir: Y - 0.1390683 Z 1.3590065
            //jumlah loop: 630 kira-kira (dari variable counter)
            //scale awal dan terakhir: 1.0008 1.6549 (rumus: start/end)
            foreach (Asset3d i in animasi_gelinding)
            {
                counter++;
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                i.translate(0, -0.08f * time * speed, 0.3f * time * speed);
                i.scale(1.0008f, 1, 1);

                //Console.WriteLine("Y " + i.objectCenter.Y);
                //Console.WriteLine("Z " + i.objectCenter.Z);
                //Console.WriteLine("Count " + counter);
                if (i.objectCenter.Z > 1.2561103f)
                {
                    double end = Math.Pow(1.0008, counter);
                    float reset_y = 0.70738404f;
                    float reset_z = -2.6526906f;
                    i.translate(0, reset_y, reset_z);
                    i.scale((float)(1.0008 / end), 1, 1);
                    counter = 0;
                }
            }

            #region animasi bola goyang
            if (bola_goyang.ElementAt(2).objectCenter.X > 0.7737516f || bola_goyang.ElementAt(2).objectCenter.X < -0.9610913f)
            {
                speed_goyang *= -1;
            }

            foreach (Asset3d i in bola_goyang)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());

                i.rotate(objectList.ElementAt(23).objectCenter, Vector3.UnitZ, speed_goyang * time);
                //Console.WriteLine("X " + bola_goyang.ElementAt(2).objectCenter.X);
            }
            #endregion

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            float time = (float)args.Time; //Deltatime ==> waktu antara frame sebelumnya ke frame berikutnya, gunakan untuk animasi

            if (!IsFocused)
            {
                return; //Reject semua input saat window bukan focus.
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            float cameraSpeed = 5.0f;

            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }

            var mouse = MouseState;
            var sensitivity = 0.1f;

            //yaw menggerakan kamera ke kanan kiri
            //pitch menggerakan kamera ke atas bawah
            if (_firstmove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstmove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }
}
