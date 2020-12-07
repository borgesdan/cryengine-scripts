//Script de movimenta��o de uma c�mera

using System;
using CryEngine;

namespace CryEngine.Game
{
	[EntityComponent(Guid="e7857f48-e6ac-59c2-5c16-4164a154c89f")]
	public class CameraManager : EntityComponent
	{
        //A entidade c�mera
		Entity camera;

		/// <summary>
		/// Called at the start of the game.
		/// </summary>
		protected override void OnGameplayStart()
		{
            //Recebo o nome da entidade c�mera, aqui nomeada de "MainCamera" na SandBox
			camera = Entity.Find("MainCamera");

            //Crio um novo objeto View e defino uma link a entidade 'camera'
            View v = View.Create();
            v.LinkTo(camera);
            //Defino que esta ser� a c�mera ativa
            v.SetActive(true);      
            
            //Se eu desejar usar qualquer c�mera ativa usuaria a classe est�tica Camera
		}        

		/// <summary>
		/// Called once every frame when the game is running.
		/// </summary>
		/// <param name="frameTime">The time difference between this and the previous frame.</param>
		protected override void OnUpdate(float frameTime)
		{
            //Processo comum de rota��o utilizando as entradas do usu�rio            
            
            var rotation = camera.Rotation;

            if (Input.KeyDown(KeyId.W))
                rotation.YawPitchRoll += new Angles3(0, 0.005f, 0);
            if (Input.KeyDown(KeyId.S))
                rotation.YawPitchRoll += new Angles3(0, -0.005f, 0);
            if (Input.KeyDown(KeyId.A))
                rotation.YawPitchRoll += new Angles3(0.005f, 0, 0);
            if (Input.KeyDown(KeyId.D))
                rotation.YawPitchRoll += new Angles3(-0.005f, 0, 0);
            if (Input.KeyDown(KeyId.R))
                rotation.YawPitchRoll += new Angles3(0, 0, 0.005f);
            if (Input.KeyDown(KeyId.F))
                rotation.YawPitchRoll += new Angles3(0, 0, -0.005f);

            camera.Rotation = rotation;
            
            //0.005f e 0.05f s�o valores arbit�trios

            //Mofica��o da posi��o da c�mera

            if (Input.KeyDown(KeyId.NP_8)) //Avan�ar: A c�mera avan�a seguindo o vetor de vis�o (camera.Forward)
                camera.Position += new Vector3(0.05f * camera.Forward.X, 0.05f * camera.Forward.Y, 0);
            if (Input.KeyDown(KeyId.NP_2)) //Recuar

                camera.Position += new Vector3(0.05f * -camera.Forward.X, 0.05f * -camera.Forward.Y, 0);
            if (Input.KeyDown(KeyId.NP_6)) //Direita: A c�mera se move lateralmente independente da rota��o YawPichRoll
                camera.Position += new Vector3(0.05f * camera.Forward.Y, 0.05f * -camera.Forward.X, 0);
            if (Input.KeyDown(KeyId.NP_4)) //Esquerda
                camera.Position += new Vector3(-0.05f * camera.Forward.Y, 0.05f * camera.Forward.X, 0);

            //Se utilizasse "camera.Position += new Vector3(0.05f, 0, 0);" a c�mera se moveria lateralmente no eixo X,
            //mas dependendo da rota��o ela poderia se mover para tr�s ou para frente.
            //Por isso o ajuste utilizando o vetor de vis�o da c�mera "Forward".

            if (Input.KeyDown(KeyId.NP_7)) //Cima
                camera.Position += new Vector3(0, 0, 0.05f);
            if (Input.KeyDown(KeyId.NP_1)) //Baixo
                camera.Position += new Vector3(0, 0, -0.05f);            
        }
	}
}