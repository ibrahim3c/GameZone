pipeline {
    agent { label 'test' }
    
    
    stages {      
        stage('Checkout') {
            steps {
                echo 'Checking out code...'
                // Add your checkout commands here
            }
        }
        
        stage('Build') {
            steps {
                echo 'Building...'
                // Add your build commands here
            }
        }

        stage("Test"){
            steps{
                echo 'Testing...'
                // Add your test commands here
            }
        }
    }
    
    post {
        success {
            echo 'Pipeline succeeded!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}
