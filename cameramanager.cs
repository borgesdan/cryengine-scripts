//Script de movimentação de uma câmera

using System;
using CryEngine;

namespace CryEngine.Game
{
	[EntityComponent(Guid="e7857f48-e6ac-59c2-5c16-4164a154c89f")]
	public class CameraManager : EntityComponent
	{
        //A entidade câmera
		Entity camera;

		/// <summary>
		/// Called at the start of the game.
		/// </summary>
		protected override void OnGameplayStart()
		{
            //Recebo o nome da entidade câmera, aqui nomeada de "MainCamera" na SandBox
			camera = Entity.Find("MainCamera");

            //Crio um novo objeto View e defino uma link a entidade 'camera'
            View v = View.Create();
            v.LinkTo(camera);
            //Defino que esta será a câmera ativa
            v.SetActive(true);      
            
            //Se eu desejar usar qualquer câmera ativa usuaria a classe estática Camera
		}        

		/// <summary>
		/// Called once every frame when the game is running.
		/// </summary>
		/// <param name="frameTime">The time difference between this and the previous frame.</param>
		protected override void OnUpdate(float frameTime)
		{
            //Processo comum de rotação utilizando as entradas do usuário            
            
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
            
            //0.005f e 0.05f são valores arbitátrios

            //Moficação da posição da câmera

            if (Input.KeyDown(KeyId.NP_8)) //Avançar: A câmera avança seguindo o vetor de visão (camera.Forward)
                camera.Position += new Vector3(0.05f * camera.Forward.X, 0.05f * camera.Forward.Y, 0);
            if (Input.KeyDown(KeyId.NP_2)) //Recuar

                camera.Position += new Vector3(0.05f * -camera.Forward.X, 0.05f * -camera.Forward.Y, 0);
            if (Input.KeyDown(KeyId.NP_6)) //Direita: A câmera se move lateralmente independente da rotação YawPichRoll
                camera.Position += new Vector3(0.05f * camera.Forward.Y, 0.05f * -camera.Forward.X, 0);
            if (Input.KeyDown(KeyId.NP_4)) //Esquerda
                camera.Position += new Vector3(-0.05f * camera.Forward.Y, 0.05f * camera.Forward.X, 0);

            //Se utilizasse "camera.Position += new Vector3(0.05f, 0, 0);" a câmera se moveria lateralmente no eixo X,
            //mas dependendo da rotação ela poderia se mover para trás ou para frente.
            //Por isso o ajuste utilizando o vetor de visão da câmera "Forward".

            if (Input.KeyDown(KeyId.NP_7)) //Cima
                camera.Position += new Vector3(0, 0, 0.05f);
            if (Input.KeyDown(KeyId.NP_1)) //Baixo
                camera.Position += new Vector3(0, 0, -0.05f);            
        }
	}
}