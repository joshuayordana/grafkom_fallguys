using System.Collections.Generic;
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

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            //warnanya violet code: rgb(238,130,238)
            var cube1 = new Asset3d(new Vector3(0.93f, 0.509f, 0.93f));
            cube1.createCuboidFlat(0, 0, 0, 0.5f); //sama dengan createBoxVertices
            objectList.Add(cube1);
            cube1.rotate(Vector3.Zero, Vector3.UnitZ, 15);

            //bawah
            var cube2 = new Asset3d(new Vector3(1, 1, 1));
            cube2.createCuboidFlat(-3, -0.4f, 0, 0.5f); //sama dengan createBoxVertices
            objectList.Add(cube2);

            //atas
            var cube3 = new Asset3d(new Vector3(1, 1, 1));
            cube3.createCuboidFlat(3, 0.4f, 0, 0.5f); //sama dengan createBoxVertices
            objectList.Add(cube3);

            //rotasi untuk dari sudut pandang lain
            cube1.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube2.rotate(Vector3.Zero, Vector3.UnitY, 90);
            cube3.rotate(Vector3.Zero, Vector3.UnitY, 90);

            //var silinder = new Asset3d(new Vector3(1.0f, 0.6f, 1.0f));
            //silinder.createCylinder(2.5f, 0.1f, 0, 0, 0);
            //objectList.Add(silinder);

            //warna orange code: (255,165,0)
            var tengah = new Asset3d(new Vector3(1, 0.647f, 0));
            tengah.createCylinder(0.15f, 0.5f, 0, 0, 3.5f);
            //tengah.translate(0, 0, 0);
            //tengah.scale(2, 1, 1);
            objectList.Add(tengah);

            //-1.5f, -0.2f, 0
            //tambahin x ngubah atas bawah, tambahin y ngubah kiri kanan, tambahin z nya jadi dekat
            var tongkat = new Asset3d(new Vector3(1.0f, 1.0f, 1.0f));
            tongkat.createCylinder(0.15f, 2, 0.1f, 0, 3.5f);
            objectList.Add(tongkat);
            tongkat.rotate(Vector3.Zero, Vector3.UnitZ, 90);

            //====================GA DIPAKE==================
            //var kotak = new Asset3d(new Vector3(1.0f, 1.0f, 1.0f));
            //kotak.createCuboid(0, -1.0f, 0, 0.2f);
            //objectList.Add(kotak);

            //var donat = new Asset3d(new Vector3(0.5f, 0.5f, 0));
            //donat.createTorus(-2, 0, 0, 0.5f, 0.5f, 50, 50);
            //objectList.Add(donat);

            foreach (Asset3d i in objectList)
            {
                i.load(Size.X, Size.Y);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            float time = (float)args.Time; //Deltatime ==> waktu antara frame sebelumnya ke frame berikutnya, gunakan untuk animasi

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // DepthBufferBit juga harus di clear karena kita memakai depth testing.

            foreach (Asset3d i in objectList)
            {
                i.render();
                //i.rotate(objectList.ElementAt(0).objectCenter, Vector3.UnitY, 10 * time);
                foreach (Asset3d j in i.child)
                {
                    //j.rotate(Vector3.Zero, Vector3.UnitY, 720 * time);
                }
            }
            objectList.ElementAt(4).rotate(objectList.ElementAt(3).objectCenter, Vector3.UnitY, 7 * time);
            //objectList.ElementAt(0).scale(0.1f, 0.1f, 0.1f);

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
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
