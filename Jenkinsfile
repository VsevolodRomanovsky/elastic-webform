#!/usr/bin/env groovy
@Library(value="jenkins-shared-libs", changelog=false)_

pipeline {
    agent none

    parameters {
            string(name: 'STAGING_DEPLOY_STAND', defaultValue: 'uat_int', description: 'Agent label to deploy on')
        }

    stages {
        stage('Build containers') {
            environment {
                CONTAINER_TAG = "${env.GIT_COMMIT}"
                API_PORT = '5089'
            }

            agent {
                label 'uat_int'
            }

            steps {
                sh "docker-compose build"
            }

            post {
                always {
                    cleanWs()
                }
            }
        }

        stage('Publish containers') {
            environment {
                CONTAINER_TAG = "${env.GIT_COMMIT}"
                API_PORT = '5089'
            }

            agent {
                label 'uat_int'
            }

            steps {
                sh "docker-compose push"
            }

            post {
                always {
                    cleanWs()
                }
            }
        }

        stage('Staging') {
            environment {
                ENVIRONMENT = 'Staging'
                CONTAINER_TAG = "${env.GIT_COMMIT}"
                API_PORT = '5089'
                ENVIRONMENT_TAG = "staging"       
            }
            agent {
                label 'uat_int'
            }


             when {
                     branch 'stage'
                  }

            steps {
                sh "docker stack deploy --compose-file docker-compose.yml --compose-file stage.yml elastic-web-form"
            }


            post {
                always {
                    cleanWs()
                }
            }
        }
    }
}
