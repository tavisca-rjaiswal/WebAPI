pipeline {
    agent any
    parameters{
        // string(defaultValue:"https://github.com/tavisca-rjaiswal/WebAPI.git", name:"GIT_URL")
        // string(defaultValue:"develop", name:"GIT_BRANCH")
        string(defaultValue: "webapi", description: 'name of docker image', name: 'docker_image_name')
		string(defaultValue: "taviscarjaiswal/webapi", description: 'repository_name', name: 'repository_name')
		string(defaultValue: "api_tag", description: 'tag name', name: 'tag_name')
    }
    stages {
        stage('Checkout'){
            steps{
//                bat "git clone -b %GIT_BRANCH% %GIT_URL%"
                checkout([$class: 'GitSCM', branches: [[name: "*/${GIT_BRANCH}" ]],
                doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], 
                userRemoteConfigs: [[url: "${GIT_URL}" ]]])
            }
        }
        stage('Restore') {
            steps {
                bat "dotnet restore"
            }
        }
        stage('Build') {
            steps {
                bat "dotnet build -p:Configuration=release -v:n"
            }
        }
        stage('Test') {
            steps {
                bat "dotnet test"
            }
        }
    	stage('publish') {
            steps {
                bat "dotnet publish"
            }
        }
	    // stage('run') {
        //     steps {
        //         bat "dotnet WebAPIExample/bin/Release/netcoreapp2.2/WebAPIExample.dll"
        //     }
        // }
	    stage('docker build') {
            steps {
                bat "docker build -t %docker_image_name% -f dockerfile ."
            }
        }

        stage('SonarQube stage') {
        	
        	steps{
        		echo 'Docker run the image pulled from dockerhub'
				bat 'dotnet C:/Users/rjaiswal/Documents/sonar-scanner/SonarScanner.MSBuild.dll begin /d:sonar.login=admin /d:sonar.password=admin /k:"f3c736460e86d1ce0e8748c22a06b38a800e89fe"'
				bat 'dotnet build'
				bat 'dotnet C:/Users/rjaiswal/Documents/sonar-scanner/SonarScanner.MSBuild.dll end /d:sonar.login=admin /d:sonar.password=admin'
        	}
        }		

        stage('Docker hub Login') {        	
        	steps{
				withCredentials([usernamePassword(credentialsId: '40519977-b1f3-4126-8184-7230ead288b0', passwordVariable: 'pass', usernameVariable: 'user')]) {					
					echo 'Docker login to dockerhub'
					bat 'docker login -p %pass% -u %user%'   	
				}	        			
        	}
        }

		stage('Docker push Image') {        	
        	steps{
        		echo 'Docker push image to dockerhub'
				bat 'docker tag %docker_image_name% %repository_name%:%tag_name%'
				bat 'docker push %repository_name%:%tag_name%' 
				bat 'docker rmi %docker_image_name%:latest'
				bat 'docker rmi %repository_name%:%tag_name%'
        	}
        }
		
		stage('Docker pull Image') {
        	
        	steps{
        		echo 'Docker pull image from dockerhub'
				bat 'docker pull %repository_name%:%tag_name%'        		
        	}
        }

	    stage('docker run') {
            steps {
                bat "docker run --rm -p 12345:12345/tcp %repository_name%:%tag_name%"
            }
        }
    }
}
